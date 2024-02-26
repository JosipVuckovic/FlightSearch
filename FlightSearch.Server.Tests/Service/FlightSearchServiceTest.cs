using FlightSearch.External.Amadeus.DTO;
using FlightSearch.External.Amadeus.Services;
using FlightSearch.Server.Models;
using FlightSearch.Server.Models.Config;
using FlightSearch.Server.Service;
using LazyCache;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FlightSearch.Server.Tests.Service;

public class FlightSearchServiceTest
{
    private readonly Mock<ILogger<FlightSearchService>> _loggerMock;
    private readonly Mock<IAmadeusApi> _amadeusApiMock;
    private readonly IAppCache _cache;
    private readonly IOptions<CacheAndDatabaseSettings> _cacheAndDatabaseSettings;

    private readonly IFlightSearchService _testedService;
    
    public FlightSearchServiceTest()
    {
        _loggerMock = new Mock<ILogger<FlightSearchService>>();
        _amadeusApiMock = new Mock<IAmadeusApi>();
        _cache = new CachingService();
        
        _testedService = new FlightSearchService(_loggerMock.Object, _amadeusApiMock.Object, _cache);
    }
    
    [Fact]
    public async Task Service_Should_Call_API_If_No_Request_Data_In_Cache()
    {
        var request = new FlightSearchRequest
        {
            OriginLocationCode = "SYD",
            DestinationLocationCode = "BKK",
            DepartureDate = "2024-06-05",
            Adults = 1,
            Children = 0,
            Infants = 0,
            NonStop = false,
            CurrencyCode = "EUR",
            MaxPrice = 500,
            Max = 250
        };
        
        await _testedService.GetFlightSearchDataAsync(request, CancellationToken.None);
        
        _amadeusApiMock.Verify(_ => _.GetFlightOffersAsync(It.Is<FlightSearchRequest>(r=> r == request), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Service_Should_Call_Not_API_If_Request_Data_In_Cache()
    {
        var request = new FlightSearchRequest
        {
            OriginLocationCode = "SYD",
            DestinationLocationCode = "BKK",
            DepartureDate = "2024-06-05",
            Adults = 1,
            Children = 0,
            Infants = 0,
            NonStop = false,
            CurrencyCode = "EUR",
            MaxPrice = 500,
            Max = 250
        };

        var cached = new FlightSearchCacheModel(request.GetHashCode(), new FlightSearchResponse(), DateTimeOffset.Now);
        _cache.Add(cached.RequestHash.ToString() , cached);
        
        await _testedService.GetFlightSearchDataAsync(request, CancellationToken.None);
        _amadeusApiMock.Verify(_ => _.GetFlightOffersAsync(It.IsAny<FlightSearchRequest>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}