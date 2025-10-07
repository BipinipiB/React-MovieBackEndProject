using MovieApp.Models.DTOs;
using MovieApp.Service.Interfaces;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services
{
    public class OpenAiService : IOpenAiService
    {

        private static string _apiKey;
        private readonly ChatClient _chatClient;
        
        public OpenAiService(SecretsService secretService)
        {
            _apiKey = secretService.GetOpenAiApiKey();
            _chatClient= new ChatClient("gpt-3.5-turbo", _apiKey);

        }

        public async Task<List<OpenAiMoviesDto>> GetMovieSuggestionsFromAI(string prompt)
        {
            var modelName = "gpt-3.5-turbo";

            var client = new OpenAI.Chat.ChatClient(modelName, _apiKey);

            var response = await client.CompleteChatAsync($"{prompt}");

            Console.Write(response);

            return new List<OpenAiMoviesDto>();
        }

    }
}
