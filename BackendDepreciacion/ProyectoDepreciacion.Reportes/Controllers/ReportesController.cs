using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoDepreciacion.Reportes.Application.UseCases;

namespace ProyectoDepreciacion.Reportes.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly GenerarReportes _generarReportes;

    public ReportesController(GenerarReportes generarReportes) => _generarReportes = generarReportes;

    [HttpGet("depreciacion/activo/{activoId:int}")]
    public async Task<IActionResult> ObtenerDepreciacion(int activoId, CancellationToken cancellationToken)
    {
        var reporte = await _generarReportes.GenerarPorActivoAsync(activoId, cancellationToken);
        return reporte is null ? NotFound($"No se encontró el activo con Id {activoId}.") : Ok(reporte);
    }

    [HttpGet("depreciacion/{anio:int}")]
    public async Task<IActionResult> ObtenerDepreciacionDelAnio(int anio, CancellationToken cancellationToken)
    {
        var reporte = await _generarReportes.GenerarConsolidadoAsync(anio, cancellationToken);
        return Ok(reporte);
    }
}
