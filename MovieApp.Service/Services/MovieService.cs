
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Models;
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
                        MoviePosterUrl = $"https://image.tmdb.org/t/p/w500{dto.PosterPath}"
                    };
                    await _movieRepo.AddAsync(movie);
                }
            }
        }

        //logic to update movies
        public Task UpdateMoviesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
