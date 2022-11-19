using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebApi.Modelos;
using WebApi.Servicios;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Servicio")]
    public class ServicioControlador : ControllerBase
    {

        private readonly ServicioServicio _servicioServicio;

        public ServicioControlador(ServicioServicio servicioServicio)
        {
            _servicioServicio = servicioServicio;
        }

        [HttpGet]
        public ActionResult<List<Servicio>> Get() => _servicioServicio.Get();

        [HttpGet("{id:length(24)}")]
        public ActionResult<Servicio> Get(string id)
        {
            var servicio = _servicioServicio.Get(id);

            if (servicio == null)
            {
                return NotFound();
            }

            return servicio;
        }

        [HttpPost]
        public ActionResult<Servicio> Create(Servicio servicio)
        {
            servicio.Id ??= BsonObjectId.GenerateNewId().ToString();
            _servicioServicio.Create(servicio);

            return CreatedAtRoute("", new { id = servicio.Id }, servicio);
        }

        [HttpPut( "{id:length(24)}")]
        public IActionResult Put(string id, Servicio servicioInf)
        {
            var servicio = _servicioServicio.Get(id);
            
            if (servicio == null)
            {
                return NotFound();
            }

            servicioInf.Id = servicio.Id;
            _servicioServicio.Update(id, servicio);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var servicio = _servicioServicio.Get(id);
            
            
            if (servicio == null)
            {
                return NotFound();
            }
            
            _servicioServicio.Remove(servicio);

            return NoContent();
        }
    }
}