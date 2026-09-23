using Microsoft.AspNetCore.Mvc;
using ProyectoDepreciación.Application.DTOs;
using ProyectoDepreciación.Application.UseCases;

namespace ProyectoDepreciación.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RegistrarUsuario _registrarUsuario;
    private readonly LoginUsuario _loginUsuario;

    public AuthController(RegistrarUsuario registrarUsuario, LoginUsuario loginUsuario)
    {
        _registrarUsuario = registrarUsuario;
        _loginUsuario = loginUsuario;
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registro(RegistroRequestDto dto)
    {
        try
        {
            await _registrarUsuario.EjecutarAsync(dto);
            return Ok(new { mensaje = "Usuario registrado correctamente." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var resultado = await _loginUsuario.EjecutarAsync(dto);
        if (resultado is null)
            return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });

        return Ok(resultado);
    }
}