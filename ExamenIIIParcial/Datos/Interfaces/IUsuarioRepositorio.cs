using Modelos;

namespace Datos.Interfaces
{
     public interface IUsuarioRepositorio
    {
        Task<bool> Nuevo(Usuario usuario);
        Task<bool> Actualizar(Usuario usuario);
        Task<bool> Eliminar(Usuario usuario);
        Task<IEnumerable<Usuario>> GetLista();

        Task<Usuario> GetPorCodigo(string codigo);
        
    }
}
