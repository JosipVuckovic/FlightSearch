using FlightSearch.External.Amadeus.DTO;
using Refit;

namespace FlightSearch.External.Amadeus.Services;

public interface IAmadeusApi
{
    [Headers("Authorization: Bearer")]
    [Get("/v2/shopping/flight-offers")]
    Task<FlightSearchResponse> GetFlightOffersAsync([Query] FlightSearchRequest request, CancellationToken cancellationToken);
}