namespace MovieApp.Models
{
    public class FavoriteMovie
    {
        public int Id { get; set; }

        //Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }

        //Foreign key to Movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
