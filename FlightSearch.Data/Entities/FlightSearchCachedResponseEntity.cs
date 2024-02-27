using FlightSearch.External.Amadeus.DTO;

namespace FlightSearch.Data.Entities;

public class FlightSearchCachedResponseEntity
{
    public int Id { get; set; }
    public int RequestHash { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public FlightSearchResponse Response { get; set; }
}