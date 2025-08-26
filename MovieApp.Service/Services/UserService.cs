using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.Repository.IRepository;
using MovieApp.Models;
using MovieApp.Models.DTOs;
using MovieApp.Service.Interfaces;



namespace MovieApp.Service.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(bool Success, string? ErrorMessage)> RegisterAsync(RegisterDto dto)
        {
            // check if user already exists with same username or email
            var UserAlreadyExists = await _userRepository.DoesUserExist(dto.Username, dto.Email);

            if (UserAlreadyExists.UsernameExists)
            {
                return (false, "Username already in use");
            }

            if (UserAlreadyExists.EmailExists)
            {
                return (false, "Email already in use");
            }

            //encrypt password with BCrypt hash
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash
            };

            //register user
            var result = await _userRepository.RegisterUserAsync(user);
 
            return (result.Success, result.ErrorMessage);
        }

        public async Task<(bool Success, string? Token, string? ErrorMessage)> LoginUser(LoginDto dto)
        {

            User user = await _userRepository.FindUserByUsernameOrEmail(dto.Identifier);

            //If user is not found return error
            if (user.Id == 0)
            {
                return (false, null, "Username or password invalid.");
            }

            //Verify password
            bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if(!passwordValid)
            {
                return (false, null, "Username or password invalid.");
            }

            string token = "Success";

            return(true, token, "login Successful");

        }


    }
}
