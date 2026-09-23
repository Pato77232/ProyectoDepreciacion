namespace ProyectoDepreciacion.Auth.Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string NombreUsuario { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Rol { get; private set; } = string.Empty;

    private Usuario() { } // EF Core

    public Usuario(string nombreUsuario, string passwordHash, string rol)
    {
        if (string.IsNullOrWhiteSpace(nombreUsuario))
            throw new ArgumentException("El nombre de usuario es obligatorio.");

        NombreUsuario = nombreUsuario;
        PasswordHash = passwordHash;
        Rol = rol;
    }
}