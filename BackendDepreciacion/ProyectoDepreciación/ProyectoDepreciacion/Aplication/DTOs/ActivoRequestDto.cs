namespace ProyectoDepreciación.Application.DTOs;

public class ActivoRequestDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal CostoAdquisicion { get; set; }
    public DateTime FechaAdquisicion { get; set; }
}