using MovieApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Interfaces
{
    public interface IUserService
    {

        Task<(bool Success, string? ErrorMessage)> RegisterAsync(RegisterDto dto);
        Task<(bool Success, string? Token, string? ErrorMessage)> AuthenticateAsync(LoginDto dto);

    }
}
