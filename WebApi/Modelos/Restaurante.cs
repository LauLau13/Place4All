using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi.Modelos
{
    public class Restaurante
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Nombre { get; set; } = "";
        public Direccion Direccion { get; set; }
        public string Descripcion { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Imagen { get; set; } = "";
        public List<Servicio> Servicio { get; set; } = new List<Servicio>();

    }

    public class IBuscaCiudad
    {
        public string Ciudad { get; set; }
    }
}
