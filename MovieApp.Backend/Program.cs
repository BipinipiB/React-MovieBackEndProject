using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Repository;
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Service;
using MovieApp.Service.Interfaces;
using MovieApp.Service.Services;
using MovieApp.Services.Interfaces;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


/* AppSettings and Configuraion */
// 1. Core config sources
// Load configuration from appsettings.json and environment-specific appsettings file
//This block of code is responsible for loading configuration settings from JSON files and environment variables.
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
      $"appsettings.{builder.Environment.EnvironmentName}.json",
      optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


//Add the Default Authentication Scheme as JWT Bearer
//This tells .NET that we will be using JWT Bearer tokens for authentication
//Also the checklist of items to validate in the token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            //validate who issued the token
            ValidateIssuer = true,
            // validate the intended audience
            ValidateAudience = true,
            //validate the token is not expired
            ValidateLifetime = true,
            //validate the signing key is valid and trusted
            ValidateIssuerSigningKey = true,
            ValidIssuer = appSettings.Issuer,
            ValidAudience = appSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(appSettings.Token))
        };
    });


// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

////register the HTTPclient..this tells us call TMDB
builder.Services.AddHttpClient<TMDBService>();

//register the TMDBRepository with the ITMDBRepository interface
//AddHttpsClient becasuee this interface is used to call TMDB API
builder.Services.AddHttpClient<TMDBRepository>();
//regis

//register the TMDBService
builder.Services.AddScoped<TMDBService>();

//register the IUserRepository with the UserRepository implementation
builder.Services.AddScoped<IUserRepository, UserRepository>();

//register the UserService
builder.Services.AddScoped<IUserService,UserService>();

//register the ITokenService
builder.Services.AddScoped<ITokenService, TokenService>();



/***************** AppSettings and Configuraion  Code second part STARTS ************************************/

//load user secrets in development environment
//Note: user secrets were added via Developer PowerShell or Command line
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

//register configuration
//this tells .NET to load the "AppSettings" section from appsettings.json into AppSettings class( inside Service model)
// and make it available via DI
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

/* AppSettings and Configuraion  Code second part ENDS */

// Register SecretService as a singleton --- A singleton service is created once for the entire lifetime of the application.
//SecretService just reads secrets from configuration — it doesn’t store per-request data.
builder.Services.AddSingleton<SecretsService>();

//register the IEmailService
builder.Services.AddScoped<IEmailService, EmailService>();



//register swagger services
builder.Services.AddEndpointsApiExplorer();
//swagger is helpful for documentation and testing
builder.Services.AddSwaggerGen();

//Enable CORS
//CORS allows frontend origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:5173") // React dev server "http://localhost:5174",
            .AllowAnyHeader()
            .AllowAnyMethod());
});

//register ApplicationDbContext
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//register IMovieRepository with MovieRepository implementation
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

//register ITMDBRepository with TMDBRepository implementation
builder.Services.AddScoped<ITMDBRepository, TMDBRepository>();

//register IMovieService
builder.Services.AddScoped<IMovieService, MovieService>();

//register MovieSyncService.
//registered MovieSyncService even though it is not Interface because
//it is used to run a background job with Hangfire
builder.Services.AddScoped<MovieSyncService>();

//register TokenService
builder.Services.AddScoped<ITokenService, TokenService>();

//register hangfire
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer();

var app = builder.Build();

// Register the MovieSyncService to run periodically
//This service will sync movies from TMDB to the local database weekly
using (var scope = app.Services.CreateScope())
{
    var recurringJobs = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    recurringJobs.AddOrUpdate<MovieSyncService>(
        "sync-tmdb-movies",
        service => service.SyncTMDBMovies(),
        Cron.Daily
    );
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    //maps the scalar API reference
    app.MapScalarApiReference();
}

//Add hangfire dashboard
//visit http://localhost:5000/hangfire to access the dashboard  5000=> Port
app.UseHangfireDashboard();

//apply CORS
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

Console.WriteLine("From Configuration:");
Console.WriteLine(builder.Configuration["AppSettings:SendGridApiKey"]); // should print key

