
using MovieApp.Models;
using MovieApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.Repository.IRepository
{
    public interface IUserRepository
    {

        Task<(bool Success, string? ErrorMessage)> RegisterUserAsync(User user);
        // Task<(bool Success, string? Token, string? ErrorMessage)> AuthenticateUserAsync(string identifier);
        Task<(bool UsernameExists, bool EmailExists)> DoesUserExist(string username, string email);

        Task<User> FindUserByUsernameOrEmail(string identifier);
    }
}
