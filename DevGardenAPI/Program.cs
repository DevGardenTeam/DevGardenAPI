using Auth;
using DevGardenAPI.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// CORS configuration
// To allow our React Client to access our API
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactNativeApp", builder =>
    {
        builder.WithOrigins("https://codefirst.iut.uca.fr/containers/DevGarden-devgardenapi")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});*/

// Add logging
builder.Logging.AddConsole();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI Configuration 
builder.Services.AddSingleton<ExternalServiceManager>();
builder.Services.AddSingleton<IOAuthHandlerFactory, OAuthHandlerFactory>();

// Client and Id secret config
/*builder.Services.Configure<OAuthClientOptions>(options =>
{
    //or use -> var ClientIdEnv = Environment.GetEnvironmentVariable("GithubClientId");

    // get github values
    var GihubClientId = builder.Configuration["GithubClientId"];
    var GithubClientSecret = builder.Configuration["GithubClientSecret"];

    // get gitlab values
    var GitlabClientId = builder.Configuration["GitlabClientId"];
    var GitlabClientSecret = builder.Configuration["GitlabClientSecret"];

    // get gitea values
    var GiteaClientId = builder.Configuration["GiteaClientId"];
    var GiteaClientSecret = builder.Configuration["GiteaClientSecret"];

    options.ClientIds = new Dictionary<string, string>
    {
        { "github", GihubClientId },
        { "gitlab", GitlabClientId },
        { "gitea", GiteaClientId }
    };

    options.ClientSecrets = new Dictionary<string, string>
    {
        { "github", GithubClientSecret },
        { "gitlab", GitlabClientSecret },
        { "gitea", GiteaClientSecret }
    };
});*/

var app = builder.Build();

app.Logger.LogInformation("Application started with updated code");
app.Logger.LogWarning("Update was successfully deployed.");

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

//app.UseHttpsRedirection();

app.UseAuthorization();

// Enable CORS
//app.UseCors("AllowReactNativeApp");

app.MapControllers();

app.Run();
