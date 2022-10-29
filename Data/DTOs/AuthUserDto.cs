using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data.DTOs
{
    public class AuthUserDto
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(256)]
        public string Password { get; set; }
    }
    public class AuthUserToken
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}