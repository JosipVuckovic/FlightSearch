using CSharpFunctionalExtensions;
using FlightSearch.External.Amadeus.DTO;
using FlightSearch.External.Amadeus.Services;
using FlightSearch.Server.Models;
using LazyCache;
using Refit;

namespace FlightSearch.Server.Service;

public interface IFlightSearchService
{
    public Task<Result<FlightSearchResponse, string>> GetFlightSearchDataAsync(FlightSearchRequest request, CancellationToken cancellationToken);
}

public class FlightSearchService : IFlightSearchService
{
    private readonly ILogger<FlightSearchService> _logger;
    private readonly IAmadeusApi _amadeusApi;
    private readonly IAppCache _cache;

    public FlightSearchService(ILogger<FlightSearchService> logger, IAmadeusApi amadeusApi, IAppCache cache)
    {
        _logger = logger;
        _amadeusApi = amadeusApi;
        _cache = cache;
    }

    public async Task<Result<FlightSearchResponse, string>> GetFlightSearchDataAsync(FlightSearchRequest request, CancellationToken cancellationToken)
    {
        try
        {
            //TODO JV: Implement looking into cache for data and save to DB based on settings
            var result = await LoadDataFromCacheOrApi(request, cancellationToken);

            //TODO JV: Map to view model

            return result.ResponseData;
        }
        catch (ApiException refitEx)
        {
            _logger.LogError(refitEx.Message); //TODO: JV if any time left, use Serilog
            return Result.Failure<FlightSearchResponse, string>(refitEx.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return Result.Failure<FlightSearchResponse, string>(ex.Message);
        }
    }

    private async Task<FlightSearchCacheModel> LoadDataFromCacheOrApi(FlightSearchRequest request, CancellationToken cancellationToken)
    {
        //TODO: JV read cache length from settings
        return await _cache.GetOrAddAsync(request.GetHashCode().ToString(), 
            async () => await GetDataFromApiAsync(request, cancellationToken),
            DateTimeOffset.Now.AddMinutes(15));
    }
    
    private async Task<FlightSearchCacheModel> GetDataFromApiAsync(FlightSearchRequest request, CancellationToken cancellationToken)
    {
        var result = await _amadeusApi.GetFlightOffersAsync(request, cancellationToken);
        return new FlightSearchCacheModel(request.GetHashCode(), result, DateTimeOffset.Now);
    }
}