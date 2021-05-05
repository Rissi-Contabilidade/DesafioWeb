using System;
using System.Collections.Generic;
using DesafioWeb.Controllers;
using DesafioWeb.Models;
using MongoDB.Driver;

namespace DesafioWeb.Services
{
    public class PeopleService
    {
        private readonly IMongoCollection<Peoples> _peoples;

        public PeopleService(IRissiAPIDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _peoples = database.GetCollection<Peoples>("Peoples");
        }
        
        public List<Peoples> Get() =>
            _peoples.Find(p => true).ToList();
        
        public Peoples Get(string id) =>
            _peoples.Find(p => p.Id == id).FirstOrDefault();
        
        public Peoples GetPerCpf(string cpf) =>
            _peoples.Find(p => p.Cpf == Convert.ToInt64(cpf)).FirstOrDefault();

        
        public Peoples Create(Peoples people)
        {
            _peoples.InsertOne(people);
            return people;
        }
        
        public void Update(string id, Peoples peopleIn) =>
            _peoples.ReplaceOne(u => u.Id == id, peopleIn);
        
        public void Remove(string id) => 
            _peoples.DeleteOne(p => p.Id == id);
        
    }
}