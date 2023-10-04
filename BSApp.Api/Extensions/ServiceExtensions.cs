using BSApp.Entities.Dtos;
using BSApp.Presentation.ActionFilters;
using BSApp.Repository.Data;
using BSApp.Service;
using BSApp.Service.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BSApp.Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(config.GetConnectionString("SqlServerConnection")));
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, LoggerManager>();
    }

    public static void ConfigureActionFilter(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilterAttribute>();
        services.AddSingleton<LogFilterAttribute>();
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(opt => {
            opt.AddPolicy("CorsPolicy", builder => 
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .WithExposedHeaders("X-Pagination")
            );
        });
    }

    public static void ConfigureDataShaper(this IServiceCollection services)
    {
        services.AddScoped<IDataShaper<BookDto>, DataShaper<BookDto>>();
    }
}
