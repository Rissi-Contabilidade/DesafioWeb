using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DesafioWeb.Models
{
    public class Ceps
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public long Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}