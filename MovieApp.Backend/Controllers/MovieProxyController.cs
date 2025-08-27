using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Models.DTOs;
using MovieApp.Service.Interfaces;
using MovieApp.Service.Services;
using MovieApp.Services.Interfaces;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace movie_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieProxyController : ControllerBase
    {
        private readonly TMDBService _tmdbService;
        private readonly IMovieRepository _movieRepo;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;


        public MovieProxyController(TMDBService tmdbService, IMovieRepository movieRepo, 
                IUserService userService, IMovieService movieService)
        {
            _tmdbService = tmdbService;
            _movieRepo = movieRepo;
            _userService = userService;
            _movieService = movieService;
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

        //POST : api/movieproxy/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var results = await _userService.LoginUser(loginDto);
           Console.WriteLine("bipinToken: " + results.Token);
            if (results.Success)
            {
               return Ok(results);
               
            }

            return Unauthorized(results);
           
        }

        //Only Autorized user can add favorite movie
        [Authorize]
        [HttpPost("favorites")]
        public async Task<IActionResult> SaveFavorite( MovieDto movie)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId2))
            {
                return Unauthorized("User ID not found or invalid in token.");
            }

            var userId = int.Parse(userIdClaim.Value);

            var result = await _movieService.AddFavoriteMovies(movie.MovieId, userId);

            return Ok("Movie saved to favorites.");
        }



    }
}
