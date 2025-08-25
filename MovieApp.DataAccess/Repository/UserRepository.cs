using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Data;
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Models;
using MovieApp.Models.DTOs;

namespace MovieApp.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserRepository( ApplicationDBContext context)
        {
            _dbContext = context;

        }

        public Task<(bool Success, string? Token, string? ErrorMessage)> AuthenticateUserAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool Success, string? ErrorMessage)> RegisterUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return (true, null);
        }
        

        //returns True when user already exists
        public async Task<(bool UsernameExists, bool EmailExists)> DoesUserExist(string username, string email)
        {
            var usernameExists = await _dbContext.Users.AnyAsync(u => u.Username == username);
            var emailExists = await _dbContext.Users.AnyAsync(u => u.Email == email);

            return (usernameExists, emailExists);
        }

    }
}
