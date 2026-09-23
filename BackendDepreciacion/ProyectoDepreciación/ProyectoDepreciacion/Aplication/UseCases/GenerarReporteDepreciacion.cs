using ProyectoDepreciación.Application.DTOs;
using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Domain.Services;

namespace ProyectoDepreciación.Application.UseCases;

public class GenerarReporteDepreciacion
{
    private readonly IActivoRepository _repository;
    private readonly CalculadoraDepreciacion _calculadora;

    public GenerarReporteDepreciacion(IActivoRepository repository, CalculadoraDepreciacion calculadora)
    {
        _repository = repository;
        _calculadora = calculadora;
    }

    public async Task<ReporteDepreciacionDto?> EjecutarAsync(int activoId)
    {
        var activo = await _repository.ObtenerPorIdAsync(activoId);
        if (activo is null) return null;

        var tabla = _calculadora.GenerarTabla(activo);

        return new ReporteDepreciacionDto
        {
            ActivoId = activo.Id,
            NombreActivo = activo.Nombre,
            CostoAdquisicion = activo.CostoAdquisicion,
            ValorResidual = activo.ValorResidual,
            Tabla = tabla.Select(t => new DepreciacionAnualDto
            {
                Anio = t.Anio,
                MesesDepreciados = t.MesesDepreciados,
                DepreciacionDelAnio = t.DepreciacionDelAnio,
                DepreciacionAcumulada = t.DepreciacionAcumulada,
                ValorEnLibros = t.ValorEnLibros
            }).ToList()
        };
    }
}