using MovieApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Interfaces
{
    public  interface IOpenAiService
    {
        Task<List<OpenAiMoviesDto>> GetMovieSuggestionsFromAI(string prompt);

    }
}
