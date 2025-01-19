using CCVAPIProyecto2.Data;
using CCVAPIProyecto2.Interfaces;
using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Repositories
{
    public class LoginRepository : ILogin
    {
        private readonly DataContext _context;
        public LoginRepository(DataContext context)
        {
            _context = context;
        }
        public Task<Usuario> Login(string nombreUsuario, string contrasenia)
        {
            throw new NotImplementedException();
        }
    }
}
