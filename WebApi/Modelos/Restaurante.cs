using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Modelos
{
    public class Restaurante
    {
        private int ID { get; set; }
        [Required]
        private string Nombre { get; set; }
        private Direccion Direccion { get; set; }//Tabla de direcciones
        private string Descripcion { get; set; }
        private string Telefono { get; set; }
        private string Imagen { get; set; }
        private list<Servicio> Servicio { get; set; }

    }
}
