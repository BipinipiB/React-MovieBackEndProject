
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Models.DTOs;

namespace MovieApp.DataAccess.Repository.IRepository
{
    public interface ITMDBRepository
    {
        Task<List<TMDBMovieDto>> GetPopularMoviesFromApiAsync();
        Task<object> SearchMoviesInAPIAsync(string query);
    }
}
