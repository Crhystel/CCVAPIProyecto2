using CCVAPIProyecto2.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CCVAPIProyecto2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogin _loginRepository;
        public LoginController(ILogin loginRepository)
        {
            _loginRepository = loginRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromQuery] string nombreUsuario,[FromQuery] string contrasenia)
        {
            var usuario = await _loginRepository.Login(nombreUsuario, contrasenia);
            if (usuario == null)
            {
                return Unauthorized("Nombre de usuario o Contraseña incorrectos");
            }
            var usuarioResponse = new
            {
                usuario.Id,
                usuario.Cedula,
                usuario.Nombre,
                usuario.NombreUsuario,
                usuario.Edad,
                Rol = usuario.Rol.ToString()
            };
            return Ok(usuario);
        }
    }
}
