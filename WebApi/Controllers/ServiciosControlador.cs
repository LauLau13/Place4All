using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebApi.Modelos;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Servicio")]
    public class ServiciosControlador : ControllerBase
    {

        private readonly ServiciosServicio _serviciosServicio;

        public ServiciosControlador(ServiciosServicio serviciosServicio)
        {
            _serviciosServicio = serviciosServicio;
        }

        [HttpGet]
        public ActionResult<List<Servicios>> Get() => _serviciosServicio.Get();

        //Se pasa por la URL un id que tiene que tener 24 caracteres ya que el BSON.Id tiene ese formato.
        [HttpGet("{id:length(24)}")]
        public ActionResult<Servicios> Get(string id)
        {
            var servicio = _serviciosServicio.Get(id);

            if (servicio == null)
            {
                return NotFound();
            }

            return servicio;
        }

        [HttpPost]
        public ActionResult<Servicios> Create(Servicios servicio)
        {
            var servicioCreado =  _serviciosServicio.Create(servicio);

            return CreatedAtRoute("", new { id = servicio.Id }, servicioCreado);
        }

        [HttpPut( "{id:length(24)}")]
        public IActionResult Put(string id, Servicios servicioInf)
        {
            var servicio = _serviciosServicio.Get(id);
            
            if (servicio == null)
            {
                return NotFound();
            }

            servicioInf.Id = servicio.Id;
            _serviciosServicio.Update(id, servicioInf);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var servicio = _serviciosServicio.Get(id);
            
            
            if (servicio == null)
            {
                return NotFound();
            }
            
            _serviciosServicio.Remove(servicio);

            return NoContent();
        }
    }
}