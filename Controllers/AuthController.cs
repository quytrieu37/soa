using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Entities;
using DatingApp.API.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AuthController(DataContext context, ITokenService tokenService){
            _tokenService = tokenService;
            _context = context;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] AuthUserDto user)
        {
            user.UserName = user.UserName.ToLower();
            if(_context.Users.Any(u=>u.Username.ToLower() == user.UserName)){
                return BadRequest("May gia mao ai?");
            }
            using var hmac = new HMACSHA512();
            var passwordByte = Encoding.UTF8.GetBytes(user.Password);
            var user1 = new User(){
                Username = user.UserName,
                PasswordHash = hmac.ComputeHash(passwordByte),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user1);
            _context.SaveChanges();
            var Token =_tokenService.CreateToken(user.UserName);
            return Ok(
                Token
                );
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] AuthUserDto user)
        {
            user.UserName = user.UserName.ToLower();
            var currentUser = _context.Users.FirstOrDefault(x=>x.Username == user.UserName);
            if(currentUser == null){
                return Unauthorized("conchonay");
            }
            using HMACSHA512 hmac = new HMACSHA512(currentUser.PasswordSalt);
            var passwordByte = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(user.Password)
            );
            for (var i = 0; i < currentUser.PasswordHash.Length; i++){
                if(currentUser.PasswordHash[i] != passwordByte[i]){
                    return Unauthorized("concho2nay");
                }
            }
            var Token =_tokenService.CreateToken(currentUser.Username);
            return Ok(
                Token
                );
        }
        [Authorize]
        [HttpGet]
        [Route("users")]
        public IActionResult Get()
        {
            return Ok(_context.Users.ToList());
        }
    }
}