using Hangfire;
using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Repository;
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Service.Services;
using MovieApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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


//register the TMDBService
builder.Services.AddScoped<TMDBService>();

//register the IUserRepository with the UserRepository implementation
builder.Services.AddScoped<IUserRepository, UserRepository>();

//register the UserService
builder.Services.AddScoped<UserService>();

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
            .WithOrigins("http://localhost:5174") // React dev server "http://localhost:5174",
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


Console.WriteLine("[Startup] TMDB sync job triggered.");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add hangfire dashboard
//visit http://localhost:5000/hangfire to access the dashboard  5000=> Port
app.UseHangfireDashboard();

//apply CORS
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
