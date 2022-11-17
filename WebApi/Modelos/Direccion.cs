using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modelos
{
    public class Direccion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        private string Calle { get; set; } = "";
        private int Numero { get; set; }
        private string Ciudad { get; set; } = "";
        private string CP { get; set; } = "";
        private string Provincia { get; set; } = "";
    }
}
