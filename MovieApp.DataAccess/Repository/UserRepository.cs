using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Models;
using MovieApp.Models.DTOs;

namespace MovieApp.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository( ApplicationDBContext context)
        {
            _context = context;

        }

        public Task<(bool Success, string? Token, string? ErrorMessage)> AuthenticateUserAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, string? ErrorMessage)> RegisterUserAsync(RegisterDto dto)
        {
            throw new NotImplementedException();
        }

        //public async Task<(bool Success, string? Token, string? ErrorMessage)> AuthenticateUserAsync(LoginDto dto)
        //{
        //    var user = await _context.Users
        //         .FirstOrDefaultAsync(u => u.Email == dto.Identifier || u.Username == dto.Identifier);

        //    if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        //    {
        //        return (false, null, "Invalid credentials");
        //    }

        //    return (true, "", null);
        //}

        //public async Task<(bool Success, string? ErrorMessage)> RegisterUserAsync(RegisterDto dto)
        //{
        //    var existingUser = await _context.Users
        //       .FirstOrDefaultAsync(u => u.Email == dto.Email || u.Username == dto.Username);

        //    if (existingUser != null)
        //    {
        //        if (existingUser.Email == dto.Email)
        //        {
        //            return (false, "Email already in use");
        //        }
        //        if (existingUser.Username == dto.Username)
        //        {
        //            return (false, "Username already in use");
        //        }
        //    }

        //    var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        //    var user = new User
        //    {
        //        Username = dto.Username,
        //        Email = dto.Email,
        //        PasswordHash = passwordHash
        //    };

        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();
        //    return (true, null);
        //}
    }
}
