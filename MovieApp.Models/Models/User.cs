using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class User
    {
        public int Id { get; set; }

        //min 5 and max 30 chars
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }    
    }
}
