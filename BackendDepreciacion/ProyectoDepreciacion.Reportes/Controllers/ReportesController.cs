using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoDepreciacion.Reportes.Application.UseCases;
using ProyectoDepreciacion.Reportes.Infrastructure;

namespace ProyectoDepreciacion.Reportes.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly GenerarReportes _generarReportes;
    private readonly ReportePdfGenerator _reportePdfGenerator;

    public ReportesController(GenerarReportes generarReportes, ReportePdfGenerator reportePdfGenerator)
    {
        _generarReportes = generarReportes;
        _reportePdfGenerator = reportePdfGenerator;
    }

    [HttpGet("depreciacion/activo/{activoId:int}")]
    public async Task<IActionResult> ObtenerDepreciacion(int activoId, CancellationToken cancellationToken)
    {
        var reporte = await _generarReportes.GenerarPorActivoAsync(activoId, cancellationToken);
        return reporte is null ? NotFound($"No se encontró el activo con Id {activoId}.") : Ok(reporte);
    }

    [HttpGet("depreciacion/activo/{activoId:int}/pdf")]
    public async Task<IActionResult> DescargarDepreciacion(int activoId, CancellationToken cancellationToken)
    {
        var reporte = await _generarReportes.GenerarPorActivoAsync(activoId, cancellationToken);
        if (reporte is null)
            return NotFound($"No se encontró el activo con Id {activoId}.");

        var pdf = _reportePdfGenerator.Generar(reporte);
        return File(pdf, "application/pdf", $"reporte-depreciacion-{activoId}.pdf");
    }

    [HttpGet("depreciacion/{anio:int}")]
    public async Task<IActionResult> ObtenerDepreciacionDelAnio(int anio, CancellationToken cancellationToken)
    {
        var reporte = await _generarReportes.GenerarConsolidadoAsync(anio, cancellationToken);
        return Ok(reporte);
    }
}
