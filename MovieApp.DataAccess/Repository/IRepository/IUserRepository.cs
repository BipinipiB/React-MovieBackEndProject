
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Models.DTOs;

namespace MovieApp.DataAccess.Repository.IRepository
{
    public interface IUserRepository
    {

        Task<(bool Success, string? ErrorMessage)> RegisterUserAsync(RegisterDto dto);
        Task<(bool Success, string? Token, string? ErrorMessage)> AuthenticateUserAsync(LoginDto dto);
    }
}
