using ProyectoDepreciación.Domain.Entities;

namespace ProyectoDepreciación.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
    Task AgregarAsync(Usuario usuario);
    Task GuardarCambiosAsync();
}