using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PeiFeira.Infrastructure.Data;

public class PeiFeiraDbContextFactory : IDesignTimeDbContextFactory<PeiFeiraDbContext>
{
    public PeiFeiraDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "PeiFeira.Api"))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<PeiFeiraDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new PeiFeiraDbContext(optionsBuilder.Options);
    }
}