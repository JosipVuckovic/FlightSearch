using FlightSearch.External.Amadeus.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightSearch.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightSearchController : ControllerBase
{
    private readonly ILogger<FlightSearchController> _logger;
    private readonly IAmadeusApi _amadeusApi;

    public FlightSearchController(ILogger<FlightSearchController> logger, IAmadeusApi amadeusApi)
    {
        _logger = logger;
        _amadeusApi = amadeusApi;
    }

    [HttpGet(Name = "GetFlightSearch")]
    public IActionResult Get()
    {
        var test = _amadeusApi.GetFlightOffers(new object()).GetAwaiter().GetResult();
        
        throw new NotImplementedException();
    }
}

