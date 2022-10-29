using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.services
{
    public interface ITokenService
    {
        string CreateToken(string username);
    }
}