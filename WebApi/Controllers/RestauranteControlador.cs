using Microsoft.AspNetCore.Mvc;
using WebApi.Modelos;
using WebApi.Servicios;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Restaurante")]
    
    public class RestauranteControlador : ControllerBase
    {

        private readonly RestauranteServicio _servicioRestaurante;
        private readonly DireccionServicio _direccionServicio;

        public RestauranteControlador(RestauranteServicio servicioRestaurante, DireccionServicio direccionServicio)
        {
            _servicioRestaurante = servicioRestaurante;
            _direccionServicio = direccionServicio;
        }

        [HttpGet]
        public ActionResult<List<Restaurante>> Get() => _servicioRestaurante.Get();

        //Se pasa por la URL un id que tiene que tener 24 caracteres ya que el BSON.Id tiene ese formato.
        [HttpGet("{id:length(24)}")]
        public ActionResult<Restaurante> Get(string id)
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
            var restauranteD = HasDireccion(restaurante);
            var restauranteCreado = _servicioRestaurante.Create(restauranteD);

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

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var restaurante = _servicioRestaurante.Get(id);


            if (restaurante == null)
            {
                return NotFound();
            }
            DeleteDireccion(restaurante);
            _servicioRestaurante.Delete(restaurante);

            return NoContent();
        }
        
        [HttpPost("search")]
        public async Task Search(IBuscaCiudad ciudad) => await _servicioRestaurante.Search(ciudad);
        
        private Restaurante HasDireccion (Restaurante restaurante)
        {
            if(restaurante.Direccion.Id == null)
            {
                var DireccionD = _direccionServicio.Create(restaurante.Direccion);
                restaurante.Direccion = DireccionD;
                return restaurante;
            }

            var direccion = _direccionServicio.Get(restaurante.Direccion.Id);
            if (direccion != null) return restaurante;
            
            direccion = _direccionServicio.Create(restaurante.Direccion);
            restaurante.Direccion = direccion;
            return restaurante;

        }

        private void DeleteDireccion (Restaurante restaurante)
        {
            _direccionServicio.Remove(restaurante.Direccion);
        }
    }
}
