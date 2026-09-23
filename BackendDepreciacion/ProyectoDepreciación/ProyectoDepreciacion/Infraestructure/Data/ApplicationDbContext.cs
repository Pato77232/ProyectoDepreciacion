using Microsoft.EntityFrameworkCore;
using ProyectoDepreciación.Domain.Entities;

namespace ProyectoDepreciación.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Activo> Activos => Set<Activo>();

    public DbSet<Usuario> Usuarios => Set<Usuario>();
}