using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Service.Services;
using MovieApp.Services.Interfaces;

namespace movie_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieProxyController : ControllerBase
    {
        private readonly TMDBService _tmdbService;
        private readonly IMovieRepository _movieRepo;


        public MovieProxyController(TMDBService tmdbService, IMovieRepository movieRepo)
        {
            _tmdbService = tmdbService;
            _movieRepo = movieRepo;
        }

        // GET: api/movieproxy/popular
        // this added "/popular" to the base route
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularMovies()
        {
            var movies = await _movieRepo.GetAllPopularMoviesFromDB();
           
            return Ok(movies);
        }

        // GET: api/movieproxy/search?query=inception
        [HttpGet("search")]
        public async Task<IActionResult> SearchMovies([FromQuery] string query)
        {
            var results = await _tmdbService.SearchMoviesAsync(query);
            return Ok(results);
        }

    }
}
