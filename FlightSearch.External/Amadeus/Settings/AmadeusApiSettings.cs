namespace FlightSearch.Server.Models.Config;

public record AmadeusApiSettings
{
    public string BaseUrl { get; set; }
    public string GrantType { get; set; }
    public string ApiKey { get; set; }
    public string ApiSecret { get; set; }
}