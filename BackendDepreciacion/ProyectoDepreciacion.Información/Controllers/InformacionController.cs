using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDepreciacion.Información.Domain.Entities;
using ProyectoDepreciacion.Información.Infrastructure.Data;

namespace ProyectoDepreciacion.Información.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InformacionController : ControllerBase
{
    private readonly InformacionDbContext _context;

    public InformacionController(InformacionDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InformacionDepreciacion>>> ObtenerTodos()
    {
        return Ok(await _context.Informaciones
            .AsNoTracking()
            .OrderByDescending(informacion => informacion.Fecha)
            .ThenByDescending(informacion => informacion.Id)
            .Take(1)
            .ToListAsync());
    }

    [Authorize(Roles = "Contador,Admin")]
    [HttpPost]
    public async Task<ActionResult<InformacionDepreciacion>> Crear(InformacionDepreciacion informacion)
    {
        informacion.Id = 0;
        _context.Informaciones.Add(informacion);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(ObtenerTodos), new { id = informacion.Id }, informacion);
    }
}