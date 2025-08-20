
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services
{
    public class MovieSyncService
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IMovieService _movieService;
        public MovieSyncService(ApplicationDBContext applicationDbContext, IMovieService movieService)
        {
            _applicationDBContext = applicationDbContext;
            _movieService = movieService;
        }

        public async Task SyncTMDBMoviesAsync()
        {
            // Logic to sync movies from TMDB to the local database

            await _movieService.CreateMoviesAsync();

        }

        public void SyncTMDBMovies()
        {
            SyncTMDBMoviesAsync().GetAwaiter().GetResult();
        }
    }
}
