using System.Net;
using AspNetCoreRateLimit;
using BSApp.Entities.Dtos;
using BSApp.Presentation.ActionFilters;
using BSApp.Repository.Data;
using BSApp.Service;
using BSApp.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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

    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt => 
        {
            opt.ReportApiVersions = true; //header api version
            opt.AssumeDefaultVersionWhenUnspecified = true; // set deafult version
            opt.DefaultApiVersion = new ApiVersion(1,0);
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s => {
            s.SwaggerDoc("v1", new OpenApiInfo {Title = "Book Store API", Version = "v1"});
            s.SwaggerDoc("v2", new OpenApiInfo {Title = "Book Store API", Version = "v2"});
        });
    }

    public static void ConfigureResponseCaching(this IServiceCollection services)
    {
        services.AddResponseCaching();
    }

    public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
    {
        services.AddHttpCacheHeaders(validationOpt => 
        {
            validationOpt.MustRevalidate = true; //not: local resource can be used if it's younger than the provided "max-age" , otherwise it must revalidate.
        });
    }

    public static void ConfigureRateLimiting(this IServiceCollection services)
    {
        
        List<RateLimitRule> rateLimitRules = new()
        {
            new RateLimitRule{ Endpoint = "*", Limit = 3, Period = "1m"}
        };
        
        services.Configure<IpRateLimitOptions>(opt => 
        {
            opt.GeneralRules = rateLimitRules;
        });
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

    }
}
