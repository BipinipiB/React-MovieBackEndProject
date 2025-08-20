namespace MovieApp.Models.DTOs
{
    public class RegisterDto
    {

        // ".. = string.Empty" initializes the string properties to an empty string instead of null
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
