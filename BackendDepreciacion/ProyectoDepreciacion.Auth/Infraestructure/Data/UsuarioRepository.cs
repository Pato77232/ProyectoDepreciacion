using Microsoft.EntityFrameworkCore;
using ProyectoDepreciacion.Auth.Application.Interfaces;
using ProyectoDepreciacion.Auth.Domain.Entities;

namespace ProyectoDepreciacion.Auth.Infrastructure.Data;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AuthDbContext _context;

    public UsuarioRepository(AuthDbContext context) => _context = context;

    public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario) =>
        await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

    public async Task AgregarAsync(Usuario usuario) => await _context.Usuarios.AddAsync(usuario);
    public async Task GuardarCambiosAsync() => await _context.SaveChangesAsync();
}