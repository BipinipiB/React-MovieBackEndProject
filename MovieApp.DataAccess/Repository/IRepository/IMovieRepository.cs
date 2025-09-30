using MovieApp.Models;
using MovieApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.Repository.IRepository
{
    //This interface defines the contract for movie repository operations.
    //Operate Records in Movie Table
    public interface IMovieRepository
    {
        //returns movie by TMDB ID
        Task<Movie> GetByTMDBIdAsync(int tmdbId);

        Task<Movie> GetMovieByMovieId(int movieId);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task<IEnumerable<Movie>> GetAllAsync();

        Task AddFavoriteMovie(FavoriteMovie favoriteMov);

        Task<IEnumerable<FavoriteMovie>> GetFavoriteMoviesByUserId(int userId);
        Task <IEnumerable<Movie>> GetAllPopularMoviesFromDB();
    }
}
