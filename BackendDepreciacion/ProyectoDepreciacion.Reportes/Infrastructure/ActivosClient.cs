using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ProyectoDepreciacion.Reportes.Application.DTOs;

namespace ProyectoDepreciacion.Reportes.Infrastructure;

public class ActivosClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ActivosClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ActivoDto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken)
    {
        using var response = await SendAsync($"Activos/{id}", cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ActivoDto>(cancellationToken);
    }

    public async Task<List<ActivoDto>> ObtenerTodosAsync(CancellationToken cancellationToken)
    {
        using var response = await SendAsync("Activos", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ActivoDto>>(cancellationToken) ?? new();
    }

    private Task<HttpResponseMessage> SendAsync(string requestUri, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var authorization = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();
        if (!string.IsNullOrWhiteSpace(authorization))
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(authorization);

        return _httpClient.SendAsync(request, cancellationToken);
    }
}
