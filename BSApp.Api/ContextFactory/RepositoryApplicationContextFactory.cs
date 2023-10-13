using BSApp.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BSApp.Api.ContextFactory;

public class RepositoryApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{environment}.json")
            .Build();
        var builder = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer(configuration.GetConnectionString("SqlServerConnection"), project => {
                project.MigrationsAssembly("BSApp.Api"); // migrations folder will be create for project assembly
            });

        return new ApplicationContext(builder.Options);
    }
}
