using Auth;
using DevGardenAPI.Managers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Get the config (appsettings.json)
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();

// CORS configuration
// To allow our React Client to access our API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactNativeApp", builder =>
    {
        builder.WithOrigins("https://codefirst.iut.uca.fr/containers/DevGarden-devgardenapi")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add logging
builder.Logging.AddConsole();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI Configuration 
builder.Services.AddSingleton<ExternalServiceManager>();
builder.Services.AddSingleton<IOAuthHandlerFactory, OAuthHandlerFactory>();



var app = builder.Build();

app.Logger.LogInformation("Application started with updated code");
app.Logger.LogWarning("Help ! ");

// Configure the HTTP request pipeline.


app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        swaggerDoc.Servers = new List<OpenApiServer>
        {
            new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" }
        };
    });
});

var base_path = Environment.GetEnvironmentVariable("SWAGGER_BASE_PATH");
app.Logger.LogInformation($" SWAGGER_BASE_PATH => {base_path}");

app.UseSwaggerUI(c =>
{
    var basePath = base_path ?? "";
    c.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "DevGardenAPI v1");
});

app.MapSwagger();

// app.UseHttpsRedirection();
app.UseAuthorization();

// Enable CORS
app.UseCors("AllowReactNativeApp");

app.MapControllers();

app.Run();
