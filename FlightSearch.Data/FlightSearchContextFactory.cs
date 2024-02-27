using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FlightSearch.Data;

// Used to keep Migrations within the Data sub project
public class FlightSearchApplicationContextFactory : IDesignTimeDbContextFactory<FlightSearchApplicationDb>
{
    public FlightSearchApplicationDb CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FlightSearchApplicationDb>();
        var configuration = new ConfigurationBuilder()
            .Build();

        optionsBuilder.UseSqlServer(
            configuration.GetConnectionString(StringConstants.StringConstants.ApplicationDb)
        );

        return new FlightSearchApplicationDb(optionsBuilder.Options);
    }
}