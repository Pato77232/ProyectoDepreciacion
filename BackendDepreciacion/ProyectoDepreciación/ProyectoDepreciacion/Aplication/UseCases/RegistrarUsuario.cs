using ProyectoDepreciación.Application.DTOs;
using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Domain.Entities;

namespace ProyectoDepreciación.Application.UseCases;

public class RegistrarUsuario
{
    private readonly IUsuarioRepository _repository;

    public RegistrarUsuario(IUsuarioRepository repository) => _repository = repository;

    public async Task EjecutarAsync(RegistroRequestDto dto)
    {
        var existente = await _repository.ObtenerPorNombreUsuarioAsync(dto.NombreUsuario);
        if (existente is not null)
            throw new InvalidOperationException("El nombre de usuario ya existe.");

        string hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var usuario = new Usuario(dto.NombreUsuario, hash, dto.Rol);

        await _repository.AgregarAsync(usuario);
        await _repository.GuardarCambiosAsync();
    }
}