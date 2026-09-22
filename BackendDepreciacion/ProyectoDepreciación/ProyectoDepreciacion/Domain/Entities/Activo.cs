namespace ProyectoDepreciación.Domain.Entities;

public class Activo
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Categoria { get; private set; } = string.Empty;
    public decimal CostoAdquisicion { get; private set; }
    public decimal ValorResidual { get; private set; }
    public DateTime FechaAdquisicion { get; private set; }
    public int VidaUtilAnios { get; private set; }
    public decimal PorcentajeAnual { get; private set; }
    public bool EstaActivo { get; private set; } = true;

    private Activo() { } // EF Core

    public Activo(string nombre, string categoria, decimal costo, decimal valorResidual,
                  DateTime fechaAdquisicion, int vidaUtilAnios, decimal porcentajeAnual)
    {
        if (costo <= 0) throw new ArgumentException("El costo debe ser mayor a cero.");
        if (valorResidual < 0 || valorResidual >= costo)
            throw new ArgumentException("El valor residual no es válido.");

        Nombre = nombre;
        Categoria = categoria;
        CostoAdquisicion = costo;
        ValorResidual = valorResidual;
        FechaAdquisicion = fechaAdquisicion;
        VidaUtilAnios = vidaUtilAnios;
        PorcentajeAnual = porcentajeAnual;
    }

    public void DarDeBaja() => EstaActivo = false;
}