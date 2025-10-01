using Microsoft.Extensions.Options;
using MovieApp.Service.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services
{
    public class EmailService : IEmailService
    {
       
        private readonly SecretsService _secretService;

        public EmailService(SecretsService secretService) 
        {
            _secretService = secretService;

        }

        public async Task SendEmail(string subject,string toEmail, string username, string message)
        {

            //get the api key from the secret service
            var apiKey = _secretService.GetSendGridApiKey();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("foldingchair_2024@outlook.com", "MyMovieApp");
            var to = new EmailAddress("bipin2nipib@gmail.com", username);
            var plainTextContent = message;
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
