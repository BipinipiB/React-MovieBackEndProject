
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Models;
using MovieApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services
{
     public class TokenService : ITokenService
    {
        //Give me the actual values from the AppSettings section of
        //appsettings.json, already mapped into my AppSettings class.
        private readonly AppSettings appSettings;

        public TokenService(IOptions<AppSettings> options)
        {
            appSettings = options.Value;
        }

        //method to create tokens
        public string CreateToken(User user)
        {
            //claims could be userId,role,username,email
            var claims = new List<Claim>
            {
                //Todo...
                //you can add more claims if needed.
                //adding only username for simplicity now
                //Probably userId is also a good idea to add
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())

            };

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(appSettings.Token));

            
            //signing credentials-token that has 64 bits
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            //descriptor that describes our token

            var tokenDescriptor = new JwtSecurityToken(
                issuer: appSettings.Issuer,
                //Users using this token is audience
                audience: appSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
             );


            //GenerateSecretKey();
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);   
        

        }

        // method to generate secret key
        public void GenerateSecretKey()
        {
            byte[] KeyBytes = RandomNumberGenerator.GetBytes(64);
            string secretKey = Convert.ToBase64String(KeyBytes);
            Console.WriteLine("Secret Key: " + secretKey);
        }

    }

    
}
