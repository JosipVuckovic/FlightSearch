using FlightSearch.External.Amadeus.DTO;
using Refit;

namespace FlightSearch.External.Amadeus.Services;

public interface IAmadeusTokenApi
{
    [Post("/v1/security/oauth2/token")]
    Task<OAuthResponse> GetAccessTokenAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> input, CancellationToken cancellationToken);
}