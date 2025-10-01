using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services
{
    /***************THIS CLASS IS TO CENTRALIZE SECRET MANAGEMENT IN ONE PLACE***************/

    public class SecretsService
    {

        private readonly AppSettings appSettings;
      
        public SecretsService(IOptions<AppSettings> options) 
        {
            appSettings = options.Value;
            Console.WriteLine($"SecretService loaded SendGridApiKey: {appSettings.SendGridApiKey}");

        }

        public string GetSendGridApiKey()
        {
            return appSettings.SendGridApiKey;
        }

        //returns a tuple with the token, issuer, and audience
        public (string token, string Issuer, string Audience ) GetJWTTokenSecret()
        {
            string token = appSettings.Token;
            string issuer = appSettings.Issuer;
            string audience = appSettings.Audience;

            return (token, issuer, audience);

        }
    }
}
