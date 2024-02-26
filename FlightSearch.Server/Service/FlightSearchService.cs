using CSharpFunctionalExtensions;
using FlightSearch.External.Amadeus.DTO;
using FlightSearch.External.Amadeus.Services;
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

    public FlightSearchService(ILogger<FlightSearchService> logger, IAmadeusApi amadeusApi)
    {
        _logger = logger;
        _amadeusApi = amadeusApi;
    }

    public async Task<Result<FlightSearchResponse, string>> GetFlightSearchDataAsync(FlightSearchRequest request, CancellationToken cancellationToken)
    {
        try
        {
            //TODO JV: Implement looking into cache for data and save to DB based on settings
            var result = await _amadeusApi.GetFlightOffersAsync(request, cancellationToken);
            
            //TODO JV: Map to view model
            
            return result;
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
}