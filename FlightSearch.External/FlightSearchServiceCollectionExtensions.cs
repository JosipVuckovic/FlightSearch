using FlightSearch.External.Amadeus.Services;
using FlightSearch.Server.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace FlightSearch.External;

public static class FlightSearchServiceCollectionExtensions
{
    public static IServiceCollection AddFlightSearchExternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AmadeusApiSettings>(configuration.GetSection(StringConstants.StringConstants.AmadeusApiSettings));
        services.AddSingleton<AuthHeaderHandler>();
        
        services.AddRefitClient<IAmadeusTokenApi>() //TODO: JV replace base address with settings call 
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://test.api.amadeus.com"));
        
        services.AddRefitClient<IAmadeusApi>() //TODO: JV replace base address with settings call
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://test.api.amadeus.com"))
            .AddHttpMessageHandler<AuthHeaderHandler>();

        return services;
    }
}