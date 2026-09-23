using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Domain.Entities;

namespace ProyectoDepreciación.Application.UseCases;

public class ObtenerActivo
{
    private readonly IActivoRepository _repository;

    public ObtenerActivo(IActivoRepository repository) => _repository = repository;

    public Task<Activo?> EjecutarAsync(int id) => _repository.ObtenerPorIdAsync(id);
}
