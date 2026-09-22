using ProyectoDepreciación.Domain.Entities;

namespace ProyectoDepreciación.Application.Interfaces;

public interface IActivoRepository
{
    Task<Activo?> ObtenerPorIdAsync(int id);
    Task<List<Activo>> ObtenerTodosAsync();
    Task AgregarAsync(Activo activo);
    Task GuardarCambiosAsync();
}