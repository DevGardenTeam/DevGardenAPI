using Auth;
using DevGardenAPI.Managers;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// CORS configuration
// To allow our React Client to access our API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactNativeApp", builder =>
    {
        builder.WithOrigins("http://localhost:19006")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add logging
builder.Logging.AddConsole();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(o => o.ApiVersionReader = new UrlSegmentApiVersionReader());

// DI Configuration 
builder.Services.AddSingleton<ExternalServiceManager>();
builder.Services.AddSingleton<IOAuthHandlerFactory, OAuthHandlerFactory>();

// Client and Id secret config
builder.Services.Configure<GithubOauthOptions>(options =>
{
    //or use -> var ClientIdEnv = Environment.GetEnvironmentVariable("GithubClientId");

    // get the client id
    var ClientId = builder.Configuration["GithubClientId"];
    if(ClientId == null)
    {
        throw new Exception("ClientId environement varibale is not set");
    }

    // get the client secret
    var ClientSecret = builder.Configuration["GithubClientSecret"];
    if(ClientSecret == null)
    {
        throw new Exception("ClientSecret environement variable is not set");
    }

    options.ClientId = ClientId;
    options.ClientSecret = ClientSecret;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable CORS
app.UseCors("AllowReactNativeApp");

app.MapControllers();

app.Run();
