using Microsoft.AspNetCore.Mvc;
using WebApi.Modelos;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Direcciones")]
    public class DireccionesControlador : ControllerBase
{
    private readonly DireccionesServicio _direccionesServicio;

    public DireccionesControlador(DireccionesServicio direccionServicio)
    {
        _direccionesServicio = direccionServicio;
    }

    [HttpGet]
    public ActionResult<List<Direcciones>> Get() => _direccionesServicio.Get();

    [HttpGet("{id:length(24)}")]
    public ActionResult<Direcciones> Get(string id) => _direccionesServicio.Get(id);

    [HttpPost]
    public ActionResult<Direcciones> Create(Direcciones direcciones)
    {
        var direccionCreada =  _direccionesServicio.Create(direcciones);

        return CreatedAtRoute("", new { id = direcciones.Id }, direccionCreada);
    }
    
    [HttpPut( "{id:length(24)}")]
    public IActionResult Put(string id, Direcciones direccionInf)
    {
        var direccion = _direccionesServicio.Get(id);
            
        if (direccion == null)
        {
            return NotFound();
        }

        direccionInf.Id = direccion.Id;
        _direccionesServicio.Update(id, direccionInf);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public IActionResult Delete(string id)
    {
        var direccion = _direccionesServicio.Get(id);
            
            
        if (direccion == null)
        {
            return NotFound();
        }
            
        _direccionesServicio.Remove(direccion);

        return NoContent();
    }
}
}
