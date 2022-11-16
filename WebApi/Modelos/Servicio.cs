using System.ComponentModel.DataAnnotations;

namespace WebApi.Modelos
{
    public class Servicio
    {
        [Required(ErrorMessage = "El ID del Servicio es obligatorio")]
        private int? ID { get; set; }
        private string? Nombre { get; set; }
        private string Descripcion { get; set; }
        

    }
}
