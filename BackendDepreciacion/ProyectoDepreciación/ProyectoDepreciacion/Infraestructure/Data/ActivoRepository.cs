using Microsoft.EntityFrameworkCore;
using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Domain.Entities;

namespace ProyectoDepreciación.Infrastructure.Data;

public class ActivoRepository : IActivoRepository
{
    private readonly ApplicationDbContext _context;

    public ActivoRepository(ApplicationDbContext context) => _context = context;

    public async Task<Activo?> ObtenerPorIdAsync(int id) => await _context.Activos.FindAsync(id);
    public async Task<List<Activo>> ObtenerTodosAsync() => await _context.Activos.ToListAsync();
    public async Task AgregarAsync(Activo activo) => await _context.Activos.AddAsync(activo);
    public async Task GuardarCambiosAsync() => await _context.SaveChangesAsync();
}