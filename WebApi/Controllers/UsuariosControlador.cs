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

        public UsuarioControlador(UsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> Get() => _usuarioServicio.Get();

        [HttpGet("{id:length(24)}")]
        public ActionResult<Usuario> Get(string id) => _usuarioServicio.Get(id);

        [HttpPost]
        public ActionResult<Usuario> Create(Usuario usuario)
        {
            var usuarioCreada = _usuarioServicio.Create(usuario);

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

            _usuarioServicio.Remove(usuario);

            return NoContent();
        }
    }
}
