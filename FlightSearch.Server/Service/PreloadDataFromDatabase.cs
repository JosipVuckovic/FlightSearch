using FlightSearch.Data;
using FlightSearch.Data.Models.Config;
using LazyCache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FlightSearch.Server.Service;

public class PreloadDataFromDatabase
{
    private readonly FlightSearchApplicationDb? _db;
    private readonly CacheAndDatabaseSettings? _settings;
    private readonly IAppCache? _cache;
    
    public PreloadDataFromDatabase(IServiceScope scope)
    {
        _db = scope.ServiceProvider.GetService<FlightSearchApplicationDb>();
        _settings = scope.ServiceProvider.GetService<IOptions<CacheAndDatabaseSettings>>()?.Value;
        _cache = scope.ServiceProvider.GetService<IAppCache>();
    }
    public void PreloadResponsesFromDbToCache()
    {
        if (_settings is not {ShouldUseDatabase: true, DatabaseNotOlderThenMinutes: not null})
            return;
        
        var responses = _db.FlightSearches.Where(_ => _.TimeStamp.AddMinutes(_settings.DatabaseNotOlderThenMinutes.Value) >= DateTimeOffset.Now).ToArray();

        foreach (var t in responses)
        {
            var remaining = (t.TimeStamp.AddMinutes(_settings.DatabaseNotOlderThenMinutes.Value) -
                             DateTimeOffset.Now.AddMinutes(_settings.CacheExpirationInMinutes));

            if (remaining.Minutes > 0)
            {
                _cache.Add(t.RequestHash.ToString(), t.Response, remaining);    
            }
        }
    }
}