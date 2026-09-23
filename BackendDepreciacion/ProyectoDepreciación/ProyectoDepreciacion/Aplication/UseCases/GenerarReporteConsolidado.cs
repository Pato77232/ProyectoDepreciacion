using ProyectoDepreciación.Application.DTOs;
using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Domain.Services;

namespace ProyectoDepreciación.Application.UseCases;

public class GenerarReporteConsolidado
{
    private readonly IActivoRepository _repository;
    private readonly CalculadoraDepreciacion _calculadora;

    public GenerarReporteConsolidado(IActivoRepository repository, CalculadoraDepreciacion calculadora)
    {
        _repository = repository;
        _calculadora = calculadora;
    }

    public async Task<List<ReporteConsolidadoItemDto>> EjecutarAsync(int anio)
    {
        var activos = await _repository.ObtenerTodosAsync();
        var resultado = new List<ReporteConsolidadoItemDto>();

        foreach (var activo in activos)
        {
            var tabla = _calculadora.GenerarTabla(activo);
            var filaDelAnio = tabla.FirstOrDefault(t => t.Anio == anio);
            if (filaDelAnio is null) continue; // el activo no tuvo depreciación ese año

            resultado.Add(new ReporteConsolidadoItemDto
            {
                ActivoId = activo.Id,
                Nombre = activo.Nombre,
                CostoAdquisicion = activo.CostoAdquisicion,
                DepreciacionDelAnio = filaDelAnio.DepreciacionDelAnio,
                DepreciacionAcumulada = filaDelAnio.DepreciacionAcumulada,
                ValorEnLibros = filaDelAnio.ValorEnLibros
            });
        }

        return resultado;
    }
}