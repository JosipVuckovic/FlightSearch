using Microsoft.AspNetCore.Mvc;

namespace FlightSearch.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightSearchController : ControllerBase
{
    public FlightSearchController()
    {
        
    }

    [HttpGet(Name = "GetFlightSearch")]
    public IActionResult Get()
    {
        throw new NotImplementedException();
    }
}

