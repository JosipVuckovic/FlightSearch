using System.Text.Json.Serialization;

namespace FlightSearch.External.Amadeus.DTO;

public record OAuthResponse(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("application_name")] string ApplicationName,
    [property: JsonPropertyName("client_id")] string ClientId,
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    [property: JsonPropertyName("state")] string State,
    [property: JsonPropertyName("scope")] string Scope);

