using Microsoft.EntityFrameworkCore;
using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Domain.Entities;

namespace ProyectoDepreciación.Infrastructure.Data;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context) => _context = context;

    public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario) =>
        await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

    public async Task AgregarAsync(Usuario usuario) => await _context.Usuarios.AddAsync(usuario);
    public async Task GuardarCambiosAsync() => await _context.SaveChangesAsync();
}