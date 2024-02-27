using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlightSearch.Data;

public class DbInitializer
{
    private readonly FlightSearchApplicationDb _context;

    public DbInitializer(IServiceScope scope)
    {
        _context = scope.ServiceProvider.GetService<FlightSearchApplicationDb>();
    }

    public async Task InitAsync()
    {
        await _context.Database.MigrateAsync();
    }
}