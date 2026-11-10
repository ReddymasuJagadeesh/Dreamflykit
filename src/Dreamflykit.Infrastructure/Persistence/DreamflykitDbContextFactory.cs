using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dreamflykit.Infrastructure.Persistence;

public sealed class DreamflykitDbContextFactory : IDesignTimeDbContextFactory<DreamflykitDbContext>
{
    public DreamflykitDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Dreamflykit.Web");
        if (!Directory.Exists(basePath))
        {
            basePath = Path.Combine(Directory.GetCurrentDirectory(), "src", "Dreamflykit.Web");
        }
        if (!Directory.Exists(basePath))
        {
            basePath = Directory.GetCurrentDirectory();
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddUserSecrets("44ef8531-2674-4ee2-b1af-729e0fa49243")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=dreamflykit_dev;Username=postgres;Password=postgres";

        var optionsBuilder = new DbContextOptionsBuilder<DreamflykitDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new DreamflykitDbContext(optionsBuilder.Options);
    }
}

