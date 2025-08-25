using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Models.DTOs;
using MovieApp.Service.Interfaces;
using MovieApp.Service.Services;
using MovieApp.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace movie_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieProxyController : ControllerBase
    {
        private readonly TMDBService _tmdbService;
        private readonly IMovieRepository _movieRepo;
        private readonly IUserService _userService;


        public MovieProxyController(TMDBService tmdbService, IMovieRepository movieRepo, IUserService userService)
        {
            _tmdbService = tmdbService;
            _movieRepo = movieRepo;
            _userService = userService;
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

        //Post: api/movieproxy/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterDto dto)
        {

            var result = await _userService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var results = await _userService.AuthenticateAsync(loginDto);

            return  Ok(results);
        }
        

        //}

    }
}
