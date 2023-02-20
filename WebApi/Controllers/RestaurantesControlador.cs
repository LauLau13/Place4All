using Microsoft.AspNetCore.Mvc;
using WebApi.Modelos;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Restaurante")]
    
    public class RestaurantesControlador : ControllerBase
    {

        private readonly RestaurantesServicio _servicioRestaurantes;
        private readonly DireccionesServicio _direccionesServicio;

        public RestaurantesControlador(RestaurantesServicio servicioRestaurantes, DireccionesServicio direccionesServicio)
        {
            _servicioRestaurantes = servicioRestaurantes;
            _direccionesServicio = direccionesServicio;
        }

        [HttpGet]
        public ActionResult<List<Restaurantes>> Get() => _servicioRestaurantes.Get();

        //Se pasa por la URL un id que tiene que tener 24 caracteres ya que el BSON.Id tiene ese formato.
        [HttpGet("{id:length(24)}")]
        public ActionResult<Restaurantes> Get(string id)
        {
            var restaurante = _servicioRestaurantes.Get(id);

            if (restaurante == null)
            {
                return NotFound();
            }

            return restaurante;
        }

        [HttpPost]
        public ActionResult<Restaurantes> Create(Restaurantes restaurante)
        {
            var restauranteD = HasDireccion(restaurante);
            var restauranteCreado = _servicioRestaurantes.Create(restauranteD);

            return CreatedAtRoute("", new { id = restaurante.Id }, restauranteCreado);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, Restaurantes restauranteInf)
        {
            var restaurante = _servicioRestaurantes.Get(id);

            if (restaurante == null)
            {
                return NotFound();
            }

            restauranteInf.Id = restaurante.Id;
            _servicioRestaurantes.Update(id, restauranteInf);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var restaurante = _servicioRestaurantes.Get(id);


            if (restaurante == null)
            {
                return NotFound();
            }
            DeleteDireccion(restaurante);
            _servicioRestaurantes.Delete(restaurante);

            return NoContent();
        }
        
        [HttpPost("search")]
        public async Task Search(IBuscaCiudad ciudad) => await _servicioRestaurantes.Search(ciudad);
        
        private Restaurantes HasDireccion (Restaurantes restaurante)
        {
            if(restaurante.Direccion.Id == null)
            {
                var DireccionD = _direccionesServicio.Create(restaurante.Direccion);
                restaurante.Direccion = DireccionD;
                return restaurante;
            }

            var direccion = _direccionesServicio.Get(restaurante.Direccion.Id);
            if (direccion != null) return restaurante;
            
            direccion = _direccionesServicio.Create(restaurante.Direccion);
            restaurante.Direccion = direccion;
            return restaurante;

        }

        private void DeleteDireccion (Restaurantes restaurante)
        {
            _direccionesServicio.Remove(restaurante.Direccion);
        }
    }
}
