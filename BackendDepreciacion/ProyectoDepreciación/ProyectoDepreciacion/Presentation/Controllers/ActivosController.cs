using Microsoft.AspNetCore.Mvc;
using ProyectoDepreciación.Application.DTOs;
using ProyectoDepreciación.Application.UseCases;

namespace ProyectoDepreciación.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivosController : ControllerBase
{
    private readonly RegistrarActivo _registrarActivo;

    public ActivosController(RegistrarActivo registrarActivo) => _registrarActivo = registrarActivo;

    [HttpPost]
    public async Task<IActionResult> Crear(ActivoRequestDto dto)
    {
        var activo = await _registrarActivo.EjecutarAsync(dto);
        return Ok(activo);
    }
}