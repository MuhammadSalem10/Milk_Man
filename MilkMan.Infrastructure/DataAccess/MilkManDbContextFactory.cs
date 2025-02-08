using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MilkMan.Infrastructure.Data;

namespace MilkMan.Infrastructure.DataAccess;
public class MilkManDbContextFactory : IDesignTimeDbContextFactory<MilkManDbContext>
{
    public MilkManDbContext CreateDbContext(string[] args)
    {
        // Assuming the API project's root path
        var apiProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "../MilkMan.API");


        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(apiProjectPath)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MilkManDbContext>();

        var connectionString = configuration.GetConnectionString("MilkManConnectionString");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string is null or empty.");
        }


        optionsBuilder.UseSqlServer(connectionString);

        return new MilkManDbContext(optionsBuilder.Options);
    }
}