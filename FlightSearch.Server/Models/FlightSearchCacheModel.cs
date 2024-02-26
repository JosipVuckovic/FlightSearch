using FlightSearch.External.Amadeus.DTO;

namespace FlightSearch.Server.Models;

public record FlightSearchCacheModel(int RequestHash, FlightSearchResponse ResponseData, DateTimeOffset TimeStamp);