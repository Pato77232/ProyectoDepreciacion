namespace ProyectoDepreciación.Application.DTOs;

public class ReporteConsolidadoItemDto
{
    public int ActivoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal CostoAdquisicion { get; set; }
    public decimal DepreciacionDelAnio { get; set; }
    public decimal DepreciacionAcumulada { get; set; }
    public decimal ValorEnLibros { get; set; }
}