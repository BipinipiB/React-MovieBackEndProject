using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MovieApp.DataAccess.Repository;
namespace MovieApp.Service.Services
{
    public class TMDBService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly TMDBRepository _tmdbRepository;


        public TMDBService(HttpClient httpClient, IConfiguration configuration, TMDBRepository tmdbRepository)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Tmdb:ApiKey"];
            _baseUrl = configuration["Tmdb:BaseUrl"];
            _tmdbRepository = tmdbRepository;
        }


        public async Task<object> GetPopularMoviesFromAPIAsync()
        {

            return await _tmdbRepository.GetPopularMoviesFromApiAsync();

            //var response = await _httpClient.GetAsync($"{_baseUrl}/movie/popular?api_key={_apiKey}");
            //response.EnsureSuccessStatusCode();

            //response.EnsureSuccessStatusCode();

            //var json = await response.Content.ReadAsStringAsync();
            //var data = JsonSerializer.Deserialize<object>(json); // Replace with a proper model later
            //return data;
        }

        public async Task<object> SearchMoviesAsync(string query)
        {

            return await _tmdbRepository.SearchMoviesInAPIAsync(query);
            //var encodedQuery = System.Web.HttpUtility.UrlEncode(query);
            //var response = await _httpClient.GetAsync($"{_baseUrl}/search/movie?api_key={_apiKey}&query={encodedQuery}");
            //response.EnsureSuccessStatusCode();
            //var json = await response.Content.ReadAsStringAsync();
            //return JsonSerializer.Deserialize<object>(json);
        }


    }
}
