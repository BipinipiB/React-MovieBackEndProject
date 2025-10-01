using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Interfaces
{
    public interface IEmailService
    {
         Task SendEmail(string subject, string toEmail, string username, string message);
    }
}
