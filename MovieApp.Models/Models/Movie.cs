namespace MovieApp.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }

        // The ID from TMDB
        public int TMDBMovieId { get; set; } 

        public string MoviePosterUrl { get; set; }

    }
}
