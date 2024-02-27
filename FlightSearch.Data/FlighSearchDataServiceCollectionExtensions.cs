using FlightSearch.Data.Models.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightSearch.Data;

public static class FlightSearchDataServiceCollectionExtensions
{
    public static IServiceCollection AddFlightSearchData(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<CacheAndDatabaseSettings>(configuration.GetSection(StringConstants.StringConstants.CacheAndDatabaseSettings));
        
        services.AddDbContext<FlightSearchApplicationDb>(
            opts => opts.UseSqlServer(
                configuration.GetConnectionString(StringConstants.StringConstants.ApplicationDb),
                x => x.MigrationsAssembly(
                    typeof(FlightSearchApplicationDb).Assembly.GetName().ToString()
                )
            )
        );

        return services;
    }
}