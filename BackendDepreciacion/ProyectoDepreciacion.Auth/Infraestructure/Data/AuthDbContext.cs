using Microsoft.EntityFrameworkCore;
using ProyectoDepreciacion.Auth.Domain.Entities;

namespace ProyectoDepreciacion.Auth.Infrastructure.Data;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
}