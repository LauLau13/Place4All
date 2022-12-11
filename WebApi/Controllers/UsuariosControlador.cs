using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IConfiguration _configuration;

        public UsuarioControlador(UsuarioServicio usuarioServicio, DireccionServicio direccionServicio, IConfiguration config)
        {
            _usuarioServicio = usuarioServicio;
            _direccionServicio = direccionServicio;
            _configuration = config;
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
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Post(Login login)
        {
            if (login.Email != null && login.Password != null)
            {
                var user = await GetUser(login.Email, login.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id),
                        new Claim("DisplayName", $"{user.Nombre} {user.Apellido}"),
                        new Claim("Email", user.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var response = new LoginResponse
                    {
                        Token= new JwtSecurityTokenHandler().WriteToken(token),
                        Usuario = user
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        private async Task<Usuario> GetUser(string email, string password) => _usuarioServicio.Login(email, password);
    }
}

public class LoginResponse
{
    public string Token { get; set; }
    public Usuario Usuario { get; set; }
}