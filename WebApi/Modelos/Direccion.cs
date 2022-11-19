using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi.Modelos
{
    public class Direccion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Calle { get; set; } = "";
        public int Numero { get; set; }
        public string Ciudad { get; set; } = "";
        public string CP { get; set; } = "";
        public string Provincia { get; set; } = "";
    }
}
