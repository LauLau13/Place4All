using Microsoft.AspNetCore.Mvc;
using WebApi.Modelos;
using WebApi.Servicios;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Direccion")]
    public class DireccionControlador : ControllerBase
{
    private readonly DireccionServicio _direccionServicio;

    public DireccionControlador(DireccionServicio direccionServicio)
    {
        _direccionServicio = direccionServicio;
    }

    [HttpGet]
    public ActionResult<List<Direccion>> Get() => _direccionServicio.Get();

    [HttpGet("{id:length(24)}")]
    public ActionResult<Direccion> Get(string id) => _direccionServicio.Get(id);

    [HttpPost]
    public ActionResult<Direccion> Create(Direccion direccion)
    {
        var direccionCreada =  _direccionServicio.Create(direccion);

        return CreatedAtRoute("", new { id = direccion.Id }, direccionCreada);
    }
    
    [HttpPut( "{id:length(24)}")]
    public IActionResult Put(string id, Direccion direccionInf)
    {
        var direccion = _direccionServicio.Get(id);
            
        if (direccion == null)
        {
            return NotFound();
        }

        direccionInf.Id = direccion.Id;
        _direccionServicio.Update(id, direccionInf);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var direccion = _direccionServicio.Get(id);
            
            
        if (direccion == null)
        {
            return NotFound();
        }
            
        _direccionServicio.Remove(direccion);

        return NoContent();
    }
}
}
