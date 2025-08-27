using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
