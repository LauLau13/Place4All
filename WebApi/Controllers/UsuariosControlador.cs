using Microsoft.AspNetCore.Mvc;
using WebApi.Modelos;
using WebApi.Servicios;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioControlador : ControllerBase
    {
        private readonly UsuarioServicio _usuarioServicio;
        private readonly DireccionServicio _direccionServicio;

        public UsuarioControlador(UsuarioServicio usuarioServicio, DireccionServicio direccionServicio)
        {
            _usuarioServicio = usuarioServicio;
            _direccionServicio = direccionServicio;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> Get() => _usuarioServicio.Get();

        [HttpGet("{id:length(24)}")]
        public ActionResult<Usuario> Get(string id) => _usuarioServicio.Get(id);

        [HttpPost]
        public ActionResult<Usuario> Create(Usuario usuario)
        {
            var usuarioD = HasDireccion(usuario);
            var usuarioCreada = _usuarioServicio.Create(usuarioD);

            return CreatedAtRoute("", new { id = usuario.Id }, usuarioCreada);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, Usuario usuarioInf)
        {
            var usuario = _usuarioServicio.Get(id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuarioInf.Id = usuario.Id;
            _usuarioServicio.Update(id, usuarioInf);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var usuario = _usuarioServicio.Get(id);


            if (usuario == null)
            {
                return NotFound();
            }
            DeleteDireccion(usuario);
            _usuarioServicio.Remove(usuario);

            return NoContent();
        }
        
        private Usuario HasDireccion(Usuario usuario)
        {
            
            if (usuario.Direccion.Id == null)
            {
                var direccionD = _direccionServicio.Create(usuario.Direccion);
                usuario.Direccion = direccionD;
                return usuario;
            }

            var direccion = _direccionServicio.Get(usuario.Direccion.Id);
            if (direccion == null)
            {
                direccion = _direccionServicio.Create(usuario.Direccion);
                usuario.Direccion = direccion;
                return usuario;
            }

            return usuario;
        }

        private void DeleteDireccion(Usuario usuario)
        {
            _direccionServicio.Remove(usuario.Direccion);
        }
    }
}
