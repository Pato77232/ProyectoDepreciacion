using ProyectoDepreciación.Application.DTOs;
using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Domain.Entities;

namespace ProyectoDepreciación.Application.UseCases;

public class RegistrarActivo
{
    private readonly IActivoRepository _repository;

    public RegistrarActivo(IActivoRepository repository) => _repository = repository;

    public async Task<Activo> EjecutarAsync(ActivoRequestDto dto)
    {
        var (vidaUtil, porcentaje) = ObtenerParametrosPorCategoria(dto.Categoria);

        decimal valorResidual = Math.Round(dto.CostoAdquisicion * 0.10m, 2);

        var activo = new Activo(dto.Nombre, dto.Categoria, dto.CostoAdquisicion,
            valorResidual, dto.FechaAdquisicion, vidaUtil, porcentaje);

        await _repository.AgregarAsync(activo);
        await _repository.GuardarCambiosAsync();
        return activo;
    }

    private static (int vidaUtil, decimal porcentaje) ObtenerParametrosPorCategoria(string categoria) =>
        categoria switch
        {
            "Inmueble" => (20, 5m),
            "Maquinaria" => (10, 10m),
            "Vehiculo" => (5, 20m),
            "Computo" => (3, 33.33m),
            _ => throw new ArgumentException("Categoría no reconocida.")
        };
}