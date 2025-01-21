using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;
using Microsoft.EntityFrameworkCore;

namespace CCVAPIProyecto2.Repositories
{
    public class LoginRepository : ILogin
    {
        private readonly DataContext _context;
        public LoginRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Usuario> Login(string nombreUsuario, string contrasenia)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == nombreUsuario);
            if(usuario==null || usuario.Contrasenia != contrasenia)
            {
                return null;
            }
            return usuario;
        }
    }
}
