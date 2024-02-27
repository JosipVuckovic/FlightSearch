using FlightSearch.External.Amadeus.Services;
using FlightSearch.Server.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Reflection;

namespace FlightSearch.External;

public static class FlightSearchServiceCollectionExtensions
{
    public static IServiceCollection AddFlightSearchExternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AmadeusApiSettings>(configuration.GetSection(StringConstants.AmadeusApiSettings));
        services.AddTransient<AuthHeaderHandler>();

        services.AddRefitClient<IAmadeusTokenApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(
                configuration.GetValue<string>(StringConstants.AmadeusApiSettingsBaseUrl)));

        
        var refitSettings = new RefitSettings
        {
            UrlParameterFormatter = new BooleanUrlParameterFormatter(),
        };
        
        services.AddRefitClient<IAmadeusApi>(refitSettings)
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration.GetValue<string>(StringConstants.AmadeusApiSettingsBaseUrl)))
            .AddHttpMessageHandler<AuthHeaderHandler>();

        return services;
    }
}

public sealed class BooleanUrlParameterFormatter : DefaultUrlParameterFormatter
{
    public override string? Format(object? parameterValue, ICustomAttributeProvider attributeProvider, Type type)
    {
        if (parameterValue is bool)
        {
            return parameterValue.ToString()!.ToLower();
        }

        return base.Format(parameterValue, attributeProvider, type);
    }
}