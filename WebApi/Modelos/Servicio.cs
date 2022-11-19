using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi.Modelos
{
    public class Servicio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Nombre { get; set; } = "";
        public string? Descripcion { get; set; }
        

    }
}
