namespace ProyectoDepreciacion.Información.Domain.Entities;

public class InformacionDepreciacion
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Areas { get; set; } = string.Empty;
}