using System.ComponentModel.DataAnnotations;

namespace WebApi.Modelos
{
    public class Usuario
    {
        [Required]
        private string ID { get; set; }
        private string Nombre { get; set; } = "";
        private string Apellido { get; set; } = "";
        private string Genero { get; set; } = "";
        private int Edad { get; set; }
        private Direccion Direccion { get; set; }
        private bool TieneDiscapacidad { get; set; } = false;
        private decimal? GradoDiscapacidad { get; set; }
        
    }
}
