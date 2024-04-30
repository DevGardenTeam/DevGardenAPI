using Auth;
using DevGardenAPI.Managers;
using Microsoft.Extensions.Options;
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
/*    c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevGardenAPI", Version = "v1" });

    // Set the base url of the api
    c.SwaggerGeneratorOptions.Servers = new List<OpenApiServer>
    {
        new OpenApiServer { Url = "https://codefirst.iut.uca.fr/containers/DevGarden-devgardenapi" },
    };
});*/

// DI Configuration 
builder.Services.AddSingleton<ExternalServiceManager>();
builder.Services.AddSingleton<IOAuthHandlerFactory, OAuthHandlerFactory>();


var app = builder.Build();

app.Logger.LogInformation("Application started with updated code");
app.Logger.LogWarning("Help ! ");

// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevGardenAPI v1");
    }
    );
} 
else 
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevGardenAPI v1");
        c.RoutePrefix = string.Empty;
    }
    );
}

// app.UseHttpsRedirection();
app.UseAuthorization();

// Enable CORS
app.UseCors("AllowReactNativeApp");

app.MapControllers();

app.Run();
