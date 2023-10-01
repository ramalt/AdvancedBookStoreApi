using BSApp.Api.Extensions;
using BSApp.Service.Contracts;
using NLog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.ReturnHttpNotAcceptable = true;
                })
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

var loggerService = app.Services.GetRequiredService<ILoggerService>();

app.ConfigureExceptionHandler(loggerService);

app.UseAuthorization();

app.MapControllers();

app.Run();
