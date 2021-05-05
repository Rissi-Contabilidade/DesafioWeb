using System.Collections.Generic;
using DesafioWeb.Controllers;
using DesafioWeb.Models;
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