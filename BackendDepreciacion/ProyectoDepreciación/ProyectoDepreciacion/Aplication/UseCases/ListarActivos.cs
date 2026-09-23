using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Domain.Entities;

namespace ProyectoDepreciación.Application.UseCases;

public class ListarActivos
{
    private readonly IActivoRepository _repository;

    public ListarActivos(IActivoRepository repository) => _repository = repository;

    public async Task<List<Activo>> EjecutarAsync() => await _repository.ObtenerTodosAsync();
}