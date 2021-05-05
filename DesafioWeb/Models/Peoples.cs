using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DesafioWeb.Models
{
    public class Peoples
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Nome { get; set; }
        [Required]
        public long Cpf { get; set; }
        
        [Required]
        public Sexo Sexo { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
        
        [Required]
        public Endereco Endereco { get; set; }

        [Required]
        public long Telefone { get; set; }
    }

    public enum Sexo
    {
        Masculino,
        Feminino
    }
    
    public class Endereco
    {
        public Ceps Cep { get; set; }
        
        public string Complemento { get; set; }
        
        public string Numero { get; set; }
        
    }
}