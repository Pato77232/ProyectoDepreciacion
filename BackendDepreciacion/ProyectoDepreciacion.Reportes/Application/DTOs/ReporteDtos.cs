namespace ProyectoDepreciacion.Reportes.Application.DTOs;

public class ActivoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal CostoAdquisicion { get; set; }
    public decimal ValorResidual { get; set; }
    public DateTime FechaAdquisicion { get; set; }
    public int VidaUtilAnios { get; set; }
    public decimal PorcentajeAnual { get; set; }
}

public class DepreciacionAnualDto
{
    public int Anio { get; set; }
    public int MesesDepreciados { get; set; }
    public decimal DepreciacionDelAnio { get; set; }
    public decimal DepreciacionAcumulada { get; set; }
    public decimal ValorEnLibros { get; set; }
}

public class ReporteDepreciacionDto
{
    public int ActivoId { get; set; }
    public string NombreActivo { get; set; } = string.Empty;
    public decimal CostoAdquisicion { get; set; }
    public decimal ValorResidual { get; set; }
    public List<DepreciacionAnualDto> Tabla { get; set; } = new();
}

public class ReporteConsolidadoItemDto
{
    public int ActivoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal CostoAdquisicion { get; set; }
    public decimal DepreciacionDelAnio { get; set; }
    public decimal DepreciacionAcumulada { get; set; }
    public decimal ValorEnLibros { get; set; }
}
