using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.services
{
    public class TokenService : ITokenService
    {
        private DataContext _context;
        public IConfiguration _configuration;
        public TokenService(IConfiguration configuration){
            _configuration = configuration;
            
        }
        public string CreateToken(string username)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, username),
                new Claim(JwtRegisteredClaimNames.Email, $"{username}@dating.app")
            };
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    symmetricKey, SecurityAlgorithms.HmacSha512Signature)
            };
 
            var tokenHandler = new JwtSecurityTokenHandler();
 
            var token = tokenHandler.CreateToken(tokenDescriptor);
 
            return tokenHandler.WriteToken(token);

        }
    }
}