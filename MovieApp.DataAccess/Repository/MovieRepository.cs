using Microsoft.EntityFrameworkCore;
using MovieApp.Models;
using MovieApp.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.DataAccess.Data;
using Microsoft.Identity.Client;


namespace MovieApp.DataAccess.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDBContext _ApplicationDbcontext;


        public MovieRepository(ApplicationDBContext applicationDBContext)
        {
            _ApplicationDbcontext = applicationDBContext;
        }

        //Task is returned as part of asynchronous operation to indicate asynchronous execution is complete
        //Add new movie in the database
        public async Task AddAsync(Movie movie)
        {
            await _ApplicationDbcontext.Movies.AddAsync(movie);
            await _ApplicationDbcontext.SaveChangesAsync();
        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> GetByTMDBIdAsync(int tmdbId)
        {

            var movie = await _ApplicationDbcontext.Movies.FirstOrDefaultAsync(m => m.TMDBMovieId == tmdbId);

            return movie;
        }

        //Update movie in database
        public async Task UpdateAsync(Movie movie)
        {
            //no await because Update method is synchronous
            _ApplicationDbcontext.Movies.Update(movie);
            await _ApplicationDbcontext.SaveChangesAsync();
        }
    }
}
