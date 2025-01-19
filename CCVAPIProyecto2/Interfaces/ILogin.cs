using CCVAPIProyecto2.Models;

namespace CCVAPIProyecto2.Interfaces
{
    public interface ILogin
    {
        Task<Usuario> Login(string nombreUsuario, string contrasenia);
    }
}
