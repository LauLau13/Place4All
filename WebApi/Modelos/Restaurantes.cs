using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApi.Modelos
{
    public class Restaurantes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Nombre { get; set; } = "";
        public Direcciones Direccion { get; set; }
        public string Descripcion { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Imagen { get; set; } = "";
        public List<Servicios> Servicios { get; set; } = new List<Servicios>();

    }

    public class IBuscaCiudad
    {
        public string Ciudad { get; set; }
    }
}
