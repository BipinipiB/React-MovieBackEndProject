using MovieApp.Models;
using MovieApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Services.Interfaces
{
    public interface IMovieService
    {
        Task CreateMoviesAsync();
        Task UpdateMoviesAsync();

        Task CreateOrUpdatePopularMovies();

        Task<bool> AddFavoriteMovies(int movieId, int userId);

        Task RemoveFavoriteMovie(MovieDto movieDto);
        Task<IEnumerable<Movie>> GetFavoriteMoviesByUserId(int userId);

    }
}
