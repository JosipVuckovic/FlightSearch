using FlightSearch.Data.Models.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FlightSearch.Data;

public class DbInitializer
{
    private readonly FlightSearchApplicationDb _context;
    private readonly CacheAndDatabaseSettings? _settings;

    public DbInitializer(IServiceScope scope)
    {
        _context = scope.ServiceProvider.GetService<FlightSearchApplicationDb>();
        _settings = scope.ServiceProvider.GetService<IOptions<CacheAndDatabaseSettings>>()?.Value;
    }

    public async Task InitAsync()
    {
        if (_settings is not {ShouldUseDatabase: true, DatabaseNotOlderThenMinutes: not null})
            return;
        
        await _context.Database.MigrateAsync();
    }
}