using Microsoft.EntityFrameworkCore;
using MovieApp.Models;
using MovieApp.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.DataAccess.Data;


namespace MovieApp.DataAccess.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDBContext _ApplicationDbcontext;


        public MovieRepository(ApplicationDBContext applicationDBContext)
        {
            _ApplicationDbcontext = applicationDBContext;
        }

        public Task AddAsync(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetByTMDBIdAsync(int tmdbId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
