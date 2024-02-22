using FlightSearch.External.Amadeus.DTO;
using Refit;

namespace FlightSearch.External.Amadeus.Services;

public interface IAmadeusTokenApi
{
    [Post("/v1/security/oauth2/token")] //TODO: JV get return type to deserialize correctly
    Task<string> GetAccessToken([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> input);
}