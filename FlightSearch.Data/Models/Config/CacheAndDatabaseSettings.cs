namespace FlightSearch.Data.Models.Config;

public record CacheAndDatabaseSettings
{
    public int CacheExpirationInMinutes { get; set; }
    public bool ShouldUseDatabase { get; set; } 
    public int? DatabaseNotOlderThenMinutes { get; set; }
}