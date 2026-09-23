using ProyectoDepreciacion.Auth.Domain.Entities;

namespace ProyectoDepreciacion.Auth.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
    Task AgregarAsync(Usuario usuario);
    Task GuardarCambiosAsync();
}