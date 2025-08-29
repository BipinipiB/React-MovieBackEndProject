
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
            //Claim is an information  of the user that gets embedded in the token
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

            //generate a key from the token in appsettings.json
            //Converts the secret string from appsettings.Token into a cryptographic key
            //It is used to sign and verify the tokn so it can't be tampered with
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(appSettings.Token));
            
            //signing credentials-token that has 64 bits
            //This wraps the key with a chosen algorith to sign the token
            //Signing ensures the token is trusted and hasn't been modified
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            //descriptor that describes our token
            var tokenDescriptor = new JwtSecurityToken(
                //who created the token
                issuer: appSettings.Issuer,
                //Users using this token is audience
                audience: appSettings.Audience,
                //Claims: info about the user
                claims: claims,
                //Expires in 1 day
                expires: DateTime.Now.AddDays(1),
                //how the token is signed
                signingCredentials: creds
             );

        

            //This converts the token object into a string
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);   
        

        }

        //ToDo: Logic to rotate keys
        // method to generate secret key
        //Note: at this point generate a key is a one time activity
        //The logic to rotate keys will be implemented later
        public void GenerateSecretKey()
        {
            byte[] KeyBytes = RandomNumberGenerator.GetBytes(64);
            string secretKey = Convert.ToBase64String(KeyBytes);
            Console.WriteLine("Secret Key: " + secretKey);
        }

    }

    
}
