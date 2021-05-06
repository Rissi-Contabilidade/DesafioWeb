using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DesafioWeb.Controllers;
using DesafioWeb.Models;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace DesafioWeb.Services
{
    public class UserService
    {
        private readonly IMongoCollection<Users> _users;

        public UserService(IRissiAPIDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<Users>("Users");
        }
        
        public string GenerateToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JWTSettings.Secret.ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.User)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
        public Users Login(string user, string password) =>
            _users.Find(u => u.User == user && u.Password == password).FirstOrDefault();


        public List<Users> Get() =>
            _users.Find(user => true).ToList();

        public Users Get(string id) =>
            _users.Find(user => user.Id == id).FirstOrDefault();

        public Users Create(Users user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, Users userIn) =>
            _users.ReplaceOne(u => u.Id == id, userIn);

        public void Remove(Users userIn) =>
            _users.DeleteOne(u => u.Id == userIn.Id);

        public void Remove(string id) => 
            _users.DeleteOne(u => u.Id == id);
    }
}