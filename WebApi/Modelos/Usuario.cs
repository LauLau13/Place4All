using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApi.Modelos
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string Genero { get; set; } = "";
        public int Edad { get; set; }
        public Direccion Direccion { get; set; }
        public bool TieneDiscapacidad { get; set; } = false;
        public decimal? GradoDiscapacidad { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

    }

    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
