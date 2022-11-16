using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modelos
{
    public class Restaurante
    {
        [Required (ErrorMessage = "El id de dirección es obligatorio")]
        public int ID { get; set; }
        public string Nombre { get; set; } = "";
        public Direccion Direccion { get; set; }
        public string Descripcion { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Imagen { get; set; } = "";
        public List<Servicio> Servicio { get; set; } = new List<Servicio>();

    }
}
