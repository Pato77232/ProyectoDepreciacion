using ProyectoDepreciacion.Reportes.Application.DTOs;
using ProyectoDepreciacion.Reportes.Application.Services;
using ProyectoDepreciacion.Reportes.Infrastructure;

namespace ProyectoDepreciacion.Reportes.Application.UseCases;

public class GenerarReportes
{
    private readonly ActivosClient _activosClient;
    private readonly CalculadoraDepreciacion _calculadora;

    public GenerarReportes(ActivosClient activosClient, CalculadoraDepreciacion calculadora)
    {
        _activosClient = activosClient;
        _calculadora = calculadora;
    }

    public async Task<ReporteDepreciacionDto?> GenerarPorActivoAsync(int activoId, CancellationToken cancellationToken)
    {
        var activo = await _activosClient.ObtenerPorIdAsync(activoId, cancellationToken);
        if (activo is null)
            return null;

        return new ReporteDepreciacionDto
        {
            ActivoId = activo.Id,
            NombreActivo = activo.Nombre,
            CostoAdquisicion = activo.CostoAdquisicion,
            ValorResidual = activo.ValorResidual,
            FechaAdquisicion = activo.FechaAdquisicion,
            Tabla = _calculadora.GenerarTabla(activo)
        };
    }

    public async Task<List<ReporteConsolidadoItemDto>> GenerarConsolidadoAsync(int anio, CancellationToken cancellationToken)
    {
        var activos = await _activosClient.ObtenerTodosAsync(cancellationToken);
        var resultado = new List<ReporteConsolidadoItemDto>();

        foreach (var activo in activos)
        {
            var filaDelAnio = _calculadora.GenerarTabla(activo).FirstOrDefault(fila => fila.Anio == anio);
            if (filaDelAnio is null)
                continue;

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
