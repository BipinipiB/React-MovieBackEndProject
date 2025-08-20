using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Models;
using MovieApp.Models.DTOs;



namespace MovieApp.Service.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(bool Success, string? ErrorMessage)> RegisterAsync(RegisterDto dto)
        {


            return await _userRepository.RegisterUserAsync(dto);


            //var existingUser = await _context.Users
            //    .FirstOrDefaultAsync(u => u.Email == dto.Email || u.Username == dto.Username);

            //if (existingUser != null)
            //{
            //    if (existingUser.Email == dto.Email)
            //    {
            //        return (false, "Email already in use");
            //    }
            //    if (existingUser.Username == dto.Username)
            //    {
            //        return (false, "Username already in use");
            //    }
            //}

            //var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            //var user = new User
            //{
            //    Username = dto.Username,
            //    Email = dto.Email,
            //    PasswordHash = passwordHash
            //};

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();
            //return (true, null);
        }

        public async Task<(bool Success, string? Token, string? ErrorMessage)> AuthenticateAsync(LoginDto dto)
        {
            return await _userRepository.AuthenticateUserAsync(dto);
        }


    }
}
