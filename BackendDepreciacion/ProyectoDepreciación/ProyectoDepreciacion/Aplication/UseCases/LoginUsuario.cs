using ProyectoDepreciación.Application.DTOs;
using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Infrastructure.Auth;

namespace ProyectoDepreciación.Application.UseCases;

public class LoginUsuario
{
    private readonly IUsuarioRepository _repository;
    private readonly JwtTokenGenerator _tokenGenerator;

    public LoginUsuario(IUsuarioRepository repository, JwtTokenGenerator tokenGenerator)
    {
        _repository = repository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginResponseDto?> EjecutarAsync(LoginRequestDto dto)
    {
        var usuario = await _repository.ObtenerPorNombreUsuarioAsync(dto.NombreUsuario);
        if (usuario is null) return null;

        bool passwordValido = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);
        if (!passwordValido) return null;

        string token = _tokenGenerator.GenerarToken(usuario);

        return new LoginResponseDto
        {
            Token = token,
            NombreUsuario = usuario.NombreUsuario,
            Rol = usuario.Rol
        };
    }
}