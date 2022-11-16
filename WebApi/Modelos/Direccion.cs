using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modelos
{
    public class Direccion
    {
        [Required (ErrorMessage = "El id de dirección es obligatorio")]
        private int ID { get; set; }
        private string Calle { get; set; } = "";
        private int Numero { get; set; }
        private string Ciudad { get; set; } = "";
        private string CP { get; set; } = "";
        private string Provincia { get; set; } = "";
    }
}
