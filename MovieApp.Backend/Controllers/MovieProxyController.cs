using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Service.Services;

namespace movie_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieProxyController : ControllerBase
    {
        private readonly TMDBService _tmdbService;

        public MovieProxyController(TMDBService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        // GET: api/movieproxy/popular
        // this added "/popular" to the base route
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularMovies()
        {
            var movies = await _tmdbService.GetPopularMoviesAsync();
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
