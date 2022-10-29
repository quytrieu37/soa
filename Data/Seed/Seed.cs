using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data.Entities;
using Newtonsoft.Json;

namespace DatingApp.API.Data.Seed
{
    public class Seed
    {
        public static void SeedUser(DataContext context)
        {
            if (context.Users.Any()) return;
            var userFile = System.IO.File.ReadAllText("Data/Seed/users.json");
            // var users = JsonSerializer.Deserialize<List<User>>(userFile);
            var users = JsonConvert.DeserializeObject<List<User>>(userFile);
            if(users == null) return;
            foreach (var user in users){
                using var hmac = new HMACSHA512();
                var passwordByte = hmac.ComputeHash(Encoding.UTF8.GetBytes("123456"));
                user.PasswordHash = passwordByte;
                user.PasswordSalt = hmac.Key;
                user.CreateAt = DateTime.Now;
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}