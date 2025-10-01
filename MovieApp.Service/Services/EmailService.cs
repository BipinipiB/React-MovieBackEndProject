using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services
{
    public class EmailService
    {
        public async Task SendEmail(string subject,string toEmail, string username, string message)
        {

            Console.WriteLine("Sending email to this email:" + toEmail);
          

            var apiKey = "";

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("foldingchair_2024@outlook.com", "MyMovieApp");
            //var subject = "Sending with SendGrid is Fun";
            //var to = new EmailAddress("test@example.com", "Example User");
            var to = new EmailAddress("toEmail", username);
            //var plainTextContent = "and easy to do anywhere, even with C#";
            var plainTextContent = message;
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
