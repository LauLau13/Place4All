namespace WebApi.Modelos;

public class Reservas
{
    public string? Id { get; set; }
    public Usuarios Usuario { get; set; }
    public Restaurantes Restaurante { get; set; }
    public int Personas { get; set; }
    public List<Servicios> Servicio { get; set; }
    public string InstruccionesEspeciales { get; set; }
}