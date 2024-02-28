using FlightSearch.External.Amadeus.DTO;
using FlightSearch.Server.Models;
using FlightSearch.Server.Service;
using Microsoft.AspNetCore.Mvc;
namespace FlightSearch.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightSearchController : ControllerBase
{
    private readonly ILogger<FlightSearchController> _logger;
    private readonly IFlightSearchService _flightSearchService;


    public FlightSearchController(ILogger<FlightSearchController> logger, 
                                  IFlightSearchService flightSearchService)
    {
        _logger = logger;
        _flightSearchService = flightSearchService;
    }

    [HttpPost(Name = "flightsearch")]
    public async Task<IActionResult> Get([FromBody]FlightSearchRequest request, CancellationToken cancellationToken)
    {
        var result = await _flightSearchService.GetFlightSearchDataAsync(request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}