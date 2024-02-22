using Newtonsoft.Json;

namespace FlightSearch.External.Amadeus.DTO;

public record OAuthResponse(
    [property: JsonProperty("type")] string Type,
    [property: JsonProperty("username")] string Username,
    [property: JsonProperty("application_name")] string ApplicationName,
    [property: JsonProperty("client_id")] string ClientId,
    [property: JsonProperty("token_type")] string TokenType,
    [property: JsonProperty("access_token")] string AccessToken,
    [property: JsonProperty("expires_in")] int ExpiresIn,
    [property: JsonProperty("state")] string State,
    [property: JsonProperty("scope")] string Scope);

