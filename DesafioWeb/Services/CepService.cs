using System;
using System.Collections.Generic;
using DesafioWeb.Controllers;
using DesafioWeb.Models;
using MongoDB.Driver;

namespace DesafioWeb.Services
{
    public class CepService
    {
        private readonly IMongoCollection<Ceps> _ceps;
        
        public CepService(IRissiAPIDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _ceps = database.GetCollection<Ceps>("Ceps");
        }
        
        public List<Ceps> Get() =>
            _ceps.Find(c => true).ToList();
        public Ceps Get(string cep) =>
            _ceps.Find(c => c.Cep == Convert.ToInt64(cep)).FirstOrDefault();

        public Ceps Create(Ceps cep)
        {
            _ceps.InsertOne(cep);
            return cep;
        }
        
        public void Update(string cep, Ceps cepIn) =>
            _ceps.ReplaceOne(u => u.Cep == Convert.ToInt64(cep), cepIn);

        
    }
}