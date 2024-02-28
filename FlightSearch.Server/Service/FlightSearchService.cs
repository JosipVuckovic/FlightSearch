using CSharpFunctionalExtensions;
using FlightSearch.Data;
using FlightSearch.Data.Entities;
using FlightSearch.Data.Models.Config;
using FlightSearch.External.Amadeus.DTO;
using FlightSearch.External.Amadeus.Services;
using FlightSearch.Server.Models;
using FlightSearch.Server.Models.Helpers;
using LazyCache;
using Microsoft.Extensions.Options;
using Refit;
using System.Text;
using System.Text.Json;

namespace FlightSearch.Server.Service;

public interface IFlightSearchService
{
    public Task<Result<ICollection<FlightSearchViewModel>, string>> GetFlightSearchDataAsync(FlightSearchRequest request, CancellationToken cancellationToken);
}

public class FlightSearchService : IFlightSearchService
{
    private readonly ILogger<FlightSearchService> _logger;
    private readonly IAmadeusApi _amadeusApi;
    private readonly IAppCache _cache;
    private readonly FlightSearchApplicationDb _db;
    private readonly CacheAndDatabaseSettings _settings;

    public FlightSearchService(ILogger<FlightSearchService> logger,
        IAmadeusApi amadeusApi,
        IAppCache cache,
        FlightSearchApplicationDb db,
        IOptions<CacheAndDatabaseSettings> settings)
    {
        _logger = logger;
        _amadeusApi = amadeusApi;
        _cache = cache;
        _db = db;
        _settings = settings.Value;
    }

    public async Task<Result<ICollection<FlightSearchViewModel>, string>> GetFlightSearchDataAsync(FlightSearchRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var flightSearch = await LoadDataFromCacheOrApi(request, cancellationToken);

            //first we directly process one ways
            var oneWay = flightSearch.ResponseData.Data.Where(_ => _.Itineraries.Count == 1 || _.OneWay is true)
                .Select(_ => _.MapToViewModel()).ToList();
            var roundTrips = flightSearch.ResponseData.Data.Where(_ => _.Itineraries.Count > 1)
                .Select(_ => _.FlattenFlightOfferWithMultipleItineraries())
                .ToList().SelectMany(s => s).ToList();

            var combined = oneWay.Concat(roundTrips).ToArray();
            return combined;
        }
        catch (ApiException refitEx)
        {
            if (refitEx.Content is not null)
            {
                var stringBuilder = new StringBuilder();
                var errors = JsonSerializer.Deserialize<Errors>(refitEx.Content) ?? null;
                foreach (var error in errors.ErrorList)
                {
                    stringBuilder.AppendLine($"{error.Detail} - Ex: {error.Source.Example}");
                }
                return Result.Failure<ICollection<FlightSearchViewModel>, string>(stringBuilder.ToString());
            }
            
            
            _logger.LogError(refitEx.Message);
            return Result.Failure<ICollection<FlightSearchViewModel>, string>(refitEx.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Result.Failure<ICollection<FlightSearchViewModel>, string>(ex.Message);
        }
    }

    private async Task<FlightSearchCacheModel> LoadDataFromCacheOrApi(FlightSearchRequest request, CancellationToken cancellationToken)
    {
        return await _cache.GetOrAddAsync(request.GetHashCode().ToString(),
            async () => await GetDataFromApiAsync(request, cancellationToken),
            DateTimeOffset.Now.AddMinutes(_settings.CacheExpirationInMinutes));
    }

    private async Task<FlightSearchCacheModel> GetDataFromApiAsync(FlightSearchRequest request, CancellationToken cancellationToken)
    {
        var flightSearchResponse = await _amadeusApi.GetFlightOffersAsync(request, cancellationToken);

        var timeStamp = DateTimeOffset.Now;
        if (_settings.ShouldUseDatabase is false)
            return new FlightSearchCacheModel(request.GetHashCode(), flightSearchResponse, timeStamp);

        await _db.FlightSearches.AddAsync(new FlightSearchCachedResponseEntity
        {
            RequestHash = request.GetHashCode(),
            TimeStamp = timeStamp,
            Response = flightSearchResponse
        }, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return new FlightSearchCacheModel(request.GetHashCode(), flightSearchResponse, timeStamp);
    }
}