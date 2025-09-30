using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models.DTOs
{
    public class MovieDto
    {

        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }

        // The ID from TMDB
        public int TMDBMovieId { get; set; }

        public string PosterPath { get; set; }

        public Boolean IsPopularToday { get; set; }



    }
}
