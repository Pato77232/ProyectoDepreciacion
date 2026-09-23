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
    private readonly ObtenerActivo _obtenerActivo;
    private readonly ListarActivos _listarActivos;

    public ActivosController(RegistrarActivo registrarActivo, ObtenerActivo obtenerActivo, ListarActivos listarActivos)
    {
        _registrarActivo = registrarActivo;
        _obtenerActivo = obtenerActivo;
        _listarActivos = listarActivos;
    }

    [Authorize(Roles = "Contador,Admin")]
    [HttpPost]
    public async Task<IActionResult> Crear(ActivoRequestDto dto)
    {
        var activo = await _registrarActivo.EjecutarAsync(dto);
        return Ok(activo);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var activo = await _obtenerActivo.EjecutarAsync(id);
        if (activo is null)
            return NotFound($"No se encontró el activo con Id {id}.");

        return Ok(activo);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var activos = await _listarActivos.EjecutarAsync();
        return Ok(activos);
    }
}