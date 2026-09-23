using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoDepreciación.Application.UseCases;

namespace ProyectoDepreciación.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly GenerarReporteConsolidado _generarReporteConsolidado;

    public ReportesController(GenerarReporteConsolidado generarReporteConsolidado) =>
        _generarReporteConsolidado = generarReporteConsolidado;

    [HttpGet("depreciacion/{anio}")]
    public async Task<IActionResult> ObtenerDepreciacionDelAnio(int anio)
    {
        var reporte = await _generarReporteConsolidado.EjecutarAsync(anio);
        return Ok(reporte);
    }
}