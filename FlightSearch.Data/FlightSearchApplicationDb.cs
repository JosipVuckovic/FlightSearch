using FlightSearch.Data.Entities;
using FlightSearch.External.Amadeus.DTO;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FlightSearch.Data;

public class FlightSearchApplicationDb : DbContext
{
    public FlightSearchApplicationDb(DbContextOptions<FlightSearchApplicationDb> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<FlightSearchCachedResponseEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.RequestHash);
            entity.Property(e => e.Response)
                .HasConversion(
                    x => FromObject(x),
                    x => ToObject(x)
            );
        });
    }
    
    public DbSet<FlightSearchCachedResponseEntity> FlightSearches { get; set; }

    private FlightSearchResponse ToObject(string response)
    {
        return JsonSerializer.Deserialize<FlightSearchResponse>(response,
            new JsonSerializerOptions {DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull}) ?? new FlightSearchResponse();
    }
    
    private string FromObject(FlightSearchResponse response)
    {
        return JsonSerializer.Serialize(response, new JsonSerializerOptions());
    }
}
