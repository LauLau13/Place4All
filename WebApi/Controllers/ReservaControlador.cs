using Microsoft.AspNetCore.Mvc;
using WebApi.Modelos;
using WebApi.Servicios;

namespace WebApi.Controllers;
[ApiController, Route("Reserva")]
public class ReservaControlador : Controller
{
    private readonly ReservaServicio _reservaServicio;

    public ReservaControlador(ReservaServicio reservaServicio)
    {
        _reservaServicio = reservaServicio;
    }

    [HttpGet]
    public async Task<List<Reserva>> Get() => await _reservaServicio.Get();

    [HttpGet("usuario/{usuarioId}")]
    public async Task<List<Reserva>> GetUserReserva(string usuarioId) =>
        await _reservaServicio.GetUserReserva(usuarioId);

    [HttpGet("{id:length(24)}")]
    public async Task<Reserva> Get(string id) => await _reservaServicio.Get(id);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Reserva reserva)
    {
        await _reservaServicio.Create(reserva);
        return CreatedAtAction(nameof(Get), new { id = reserva.Id }, reserva);
    }

    [HttpPut]
    public async Task<IActionResult> Put(string id, Reserva reservaInf)
    {
        var reserva = await _reservaServicio.Get(id);

        if (reserva == null)
        {
            return NotFound();
        }

        reservaInf.Id = reserva.Id;
        await _reservaServicio.Update(id, reserva);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Remove(Reserva reserva)
    {
        var reservaChecked = await _reservaServicio.Get(reserva.Id);
        if (reservaChecked == null)
        {
            return NoContent();
        }
        
        _reservaServicio.Remove(reservaChecked);

        return NoContent();
    }
}