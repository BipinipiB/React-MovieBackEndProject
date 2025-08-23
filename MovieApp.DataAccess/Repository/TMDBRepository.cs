using MovieApp.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MovieApp.Models.DTOs;

namespace MovieApp.DataAccess.Repository
{
    public class TMDBRepository : ITMDBRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public TMDBRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Tmdb:ApiKey"];
            _baseUrl = configuration["Tmdb:BaseUrl"];
        }

        //returns current popular movies in TMDB
        public async Task<List<TMDBMovieDto>> GetPopularMoviesFromApiAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/movie/popular?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var tmdbResponse = JsonSerializer.Deserialize<TMDBResponse>(json, options);

            return tmdbResponse?.Results ?? new List<TMDBMovieDto>();

        }

        public async Task<object> SearchMoviesInAPIAsync(string query)
        {
            var encodedQuery = System.Web.HttpUtility.UrlEncode(query);
            var response = await _httpClient.GetAsync($"{_baseUrl}/search/movie?api_key={_apiKey}&query={encodedQuery}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<object>(json);
        }
    }
}
