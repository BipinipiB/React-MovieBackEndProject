
using Microsoft.Identity.Client;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Models;
using MovieApp.Models.DTOs;
using MovieApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services
{
    public class MovieService : IMovieService
    {
        private readonly ITMDBRepository _tMDBRepository;
        private readonly IMovieRepository _movieRepo;

        public MovieService(ITMDBRepository tMDBRepository, IMovieRepository movieRepo)
        {
            _tMDBRepository = tMDBRepository;
            _movieRepo = movieRepo;
        }

        //creates movies in the database
        public async Task CreateMoviesAsync()
        {
            var tmdbResponse = _tMDBRepository.GetPopularMoviesFromApiAsync();

            foreach (var dto in tmdbResponse.Result)
            {
                //Check if the movie already exists in the database using TMDB ID
                var existingMovie = await _movieRepo.GetByTMDBIdAsync(dto.Id);

                //If the movie does not exist, create a new movie record
                if (existingMovie == null)
                {
                    var movie = new Movie
                    {
                        TMDBMovieId = dto.Id,
                        Title = dto.Title,
                        Description = dto.Overview,
                        ReleaseDate = DateTime.Parse(dto.ReleaseDate),
                        PosterPath = $"https://image.tmdb.org/t/p/w500{dto.PosterPath}",
                        IsPopularToday = true
                    };
                    await _movieRepo.AddAsync(movie);
                }
            }
        }

        public async Task CreateOrUpdatePopularMovies()
        {
            //1. get all movies set as popular in local db

            var currentPopularMovies = await _movieRepo.GetAllPopularMoviesFromDB();

            //2. set all movies as not popular IsPopularToday = False

            foreach(Movie m in currentPopularMovies)
            {
                m.IsPopularToday = false;
               await  _movieRepo.UpdateAsync(m);
            }

            //3. Get popular movies for the day from TMDB API
            var tmdbResponse = _tMDBRepository.GetPopularMoviesFromApiAsync();

            //4. check for each movie if they exist in local Db
            foreach(var dto in tmdbResponse.Result)
            {
                var existingMovie = await _movieRepo.GetByTMDBIdAsync(dto.Id);

                //4.1) If the movie does not exist, create a new movie record
                if (existingMovie == null)
                {
                    var movie = new Movie
                    {
                        TMDBMovieId = dto.Id,
                        Title = dto.Title,
                        Description = dto.Overview,
                        ReleaseDate = DateTime.Parse(dto.ReleaseDate),
                        PosterPath = dto.PosterPath, //$"https://image.tmdb.org/t/p/w500{dto.PosterPath}",
                        IsPopularToday = true
                    };
                    await _movieRepo.AddAsync(movie);
                }//4.2) If the movie exists, update its IsPopularToday flag to true
                else
                {
                    existingMovie.IsPopularToday = true;
                    await _movieRepo.UpdateAsync(existingMovie);
                }
            }

        }

        //Add movie to user's favorite list
        public async Task<bool>  AddFavoriteMovies(int movieId, int userId)
        {

            var userFavoriteMovie = new FavoriteMovie()
            {
                UserId = userId,
                MovieId = movieId
            };

           await _movieRepo.AddFavoriteMovie(userFavoriteMovie);

            return true;
        }

        public async Task<IEnumerable<Movie>> GetFavoriteMoviesByUserId(int userId)
        {
            List<Movie> favMovies = new List<Movie>();

            var favoriteMovies = await _movieRepo.GetFavoriteMoviesByUserId(userId);

            foreach (FavoriteMovie FM in favoriteMovies)
            {
                var movie = await _movieRepo.GetMovieByMovieId(FM.MovieId);
                if (movie != null)
                {
                    favMovies.Add(movie);
                }

            }

            return favMovies;
        }

        //Remove movie from user's favorite list
        public Task RemoveFavoriteMovie(MovieDto movieDto)
        {
            throw new NotImplementedException();
        }


        //logic to update movies
        public Task UpdateMoviesAsync()
        {
            throw new NotImplementedException();
        }



    }
}
