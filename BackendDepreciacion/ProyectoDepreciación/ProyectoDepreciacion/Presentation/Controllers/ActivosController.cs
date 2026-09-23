using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoDepreciación.Application.DTOs;
using ProyectoDepreciación.Application.UseCases;

namespace ProyectoDepreciación.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ActivosController : ControllerBase
{
    private readonly RegistrarActivo _registrarActivo;
    private readonly GenerarReporteDepreciacion _generarReporte;
    private readonly ListarActivos _listarActivos;

    public ActivosController(RegistrarActivo registrarActivo, GenerarReporteDepreciacion generarReporte, ListarActivos listarActivos)
    {
        _registrarActivo = registrarActivo;
        _generarReporte = generarReporte;
        _listarActivos = listarActivos;
    }

    [Authorize(Roles = "Contador,Admin")]
    [HttpPost]
    public async Task<IActionResult> Crear(ActivoRequestDto dto)
    {
        var activo = await _registrarActivo.EjecutarAsync(dto);
        return Ok(activo);
    }

    [HttpGet("{id}/depreciacion")]
    public async Task<IActionResult> ObtenerDepreciacion(int id)
    {
        var reporte = await _generarReporte.EjecutarAsync(id);
        if (reporte is null)
            return NotFound($"No se encontró el activo con Id {id}.");

        return Ok(reporte);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var activos = await _listarActivos.EjecutarAsync();
        return Ok(activos);
    }
}