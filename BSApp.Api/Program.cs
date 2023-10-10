using BSApp.Api.Extensions;
using BSApp.Service.Contracts;
using NLog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.ReturnHttpNotAcceptable = true;
                })
                // .AddXmlDataContractSerializerFormatters()
                .AddApplicationPart(typeof(BSApp.Presentation.AssemblyReference).Assembly)
                .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ConfigureActionFilter();
builder.Services.ConfigureCors();
builder.Services.ConfigureDataShaper();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureResponseCaching();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s => 
    {
        s.SwaggerEndpoint("v1/swagger.json","Book Store API Version 1.0");
        s.SwaggerEndpoint("v2/swagger.json","Book Store API Version 2.0");
    });
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}
app.UseCors("CorsPolicy");

app.UseResponseCaching();

var loggerService = app.Services.GetRequiredService<ILoggerService>();

app.ConfigureExceptionHandler(loggerService);

app.UseAuthorization();
app.MapControllers();

app.Run();
