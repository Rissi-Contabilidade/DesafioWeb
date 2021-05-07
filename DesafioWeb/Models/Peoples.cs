using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        [BsonRepresentation(representation: BsonType.String)]
        public Sexo Sexo { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
        
        [Required]
        public Adress Endereco { get; set; }

        [Required]
        public long Telefone { get; set; }
    }

    [JsonConverter(converterType: typeof(StringEnumConverter))]
    public enum Sexo
    {
        [EnumMember(Value = "Masculino")]
        Masculino,
        [EnumMember(Value = "Feminino")]
        Feminino
    }
}