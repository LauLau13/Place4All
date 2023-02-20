namespace WebApi.Modelos;

public class Reserva
{
    public string? Id { get; set; }
    public Usuario Usuario { get; set; }
    public Restaurante Restaurante { get; set; }
    public int Personas { get; set; }
    public List<Servicio> Servicios { get; set; }
    public string InstruccionesEspeciales { get; set; }
    public DateTime FechaReserva { get; set; }
}