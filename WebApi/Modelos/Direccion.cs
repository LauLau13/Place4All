using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Modelos
{
    public class Direccion
    {
        
        private int ID { get; set; }
        [Required]
        private string? Calle { get; set; }
        private int Numero { get; set; }
        private string? Ciudad { get; set; }
        private string? CP { get; set; }
        private string? Provincia { get; set; }
    }
}
