namespace MovieApp.Models.DTOs
{
    public class LoginDto
    {
        //login using either email or username
        public string Identifier { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        
    }
}
