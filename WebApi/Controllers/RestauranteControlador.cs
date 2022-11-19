using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebApi.Modelos;
using WebApi.Servicios;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Restaurante")]
    
    public class RestauranteControlador : ControllerBase
    {

        private readonly RestauranteServicio _servicioRestaurante;
        public RestauranteControlador(RestauranteServicio servicioRestaurante)
        {
            _servicioRestaurante = servicioRestaurante;
        }

        [HttpGet]
        public ActionResult<List<Restaurante>> Get() => _servicioRestaurante.Get();

        //Se pasa por la URL un id que tiene que tener 24 caracteres ya que el BSON.Id tiene ese formato.
        [HttpGet("{id:length(24)}")]
        public ActionResult<Restaurante> Get(int id)
        {
            var restaurante = _servicioRestaurante.Get(id);

            if (restaurante == null)
            {
                return NotFound();
            }

            return restaurante;
        }

        [HttpPost]
        public ActionResult<Restaurante> Create(Restaurante restaurante)
        {
            var restauranteCreado = _servicioRestaurante.Create(restaurante);

            return CreatedAtRoute("", new { id = restaurante.Id }, restauranteCreado);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, Restaurante restauranteInf)
        {
            var restaurante = _servicioRestaurante.Get(id);

            if (restaurante == null)
            {
                return NotFound();
            }

            restauranteInf.Id = restaurante.Id;
            _servicioRestaurante.Update(id, restauranteInf);

            return NoContent();
        }

        // DELETE api/<RestauranteControlador>/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var restaurante = _servicioRestaurante.Get(id);


            if (restaurante == null)
            {
                return NotFound();
            }

            _servicioRestaurante.Remove(restaurante);

            return NoContent();
        }
    }
}
