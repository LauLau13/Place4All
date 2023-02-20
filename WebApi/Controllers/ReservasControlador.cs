using Microsoft.AspNetCore.Mvc;
using WebApi.Modelos;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("Reserva")]
public class ReservasControlador : Controller
{
    private readonly ReservasServicio _reservasServicio;

    public ReservasControlador(ReservasServicio reservasServicio)
    {
        _reservasServicio = reservasServicio;
    }

    [HttpGet]
    public async Task<List<Reservas>> Get() => await _reservasServicio.Get();

    [HttpGet("usuario/{usuarioId}")]
    public async Task<List<Reservas>> GetUserReserva(string usuarioId) =>
        await _reservasServicio.GetUserReserva(usuarioId);

    [HttpGet("{id:length(24)}")]
    public async Task<Reservas> Get(string id) => await _reservasServicio.Get(id);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Reservas reserva)
    {
        await _reservasServicio.Create(reserva);
        return CreatedAtAction(nameof(Get), new { id = reserva.Id }, reserva);
    }

    [HttpPut]
    public async Task<IActionResult> Put(string id, Reservas reservaInf)
    {
        var reserva = await _reservasServicio.Get(id);

        if (reserva == null)
        {
            return NotFound();
        }

        reservaInf.Id = reserva.Id;
        await _reservasServicio.Update(id, reserva);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Remove(Reservas reserva)
    {
        var reservaChecked = await _reservasServicio.Get(reserva.Id);
        if (reservaChecked == null)
        {
            return NoContent();
        }
        
        _reservasServicio.Remove(reservaChecked);

        return NoContent();
    }
}