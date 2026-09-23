using Microsoft.EntityFrameworkCore;
using ProyectoDepreciacion.Información.Domain.Entities;

namespace ProyectoDepreciacion.Información.Infrastructure.Data;

public class InformacionDbContext : DbContext
{
    public InformacionDbContext(DbContextOptions<InformacionDbContext> options) : base(options) { }

    public DbSet<InformacionDepreciacion> Informaciones => Set<InformacionDepreciacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InformacionDepreciacion>(entity =>
        {
            entity.Property(informacion => informacion.Descripcion).HasMaxLength(250).IsRequired();
            entity.Property(informacion => informacion.Areas).HasMaxLength(250).IsRequired();
            entity.Property(informacion => informacion.Fecha).HasColumnType("date");
        });
    }
}