using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoDepreciacion.Gateway.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class GatewayController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GatewayController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

    [AllowAnonymous]
    [HttpPost("Auth/login")]
    public Task<IActionResult> Login([FromBody] JsonElement body, CancellationToken cancellationToken) =>
        ForwardAsync("AuthService", HttpMethod.Post, "Auth/login", body, cancellationToken);

    [AllowAnonymous]
    [HttpPost("Auth/registro")]
    public Task<IActionResult> Registro([FromBody] JsonElement body, CancellationToken cancellationToken) =>
        ForwardAsync("AuthService", HttpMethod.Post, "Auth/registro", body, cancellationToken);

    [HttpGet("Activos")]
    public Task<IActionResult> ObtenerActivos(CancellationToken cancellationToken) =>
        ForwardAsync("ActivosService", HttpMethod.Get, "Activos", null, cancellationToken);

    [HttpGet("Activos/{id:int}")]
    public Task<IActionResult> ObtenerActivo(int id, CancellationToken cancellationToken) =>
        ForwardAsync("ActivosService", HttpMethod.Get, $"Activos/{id}", null, cancellationToken);

    [HttpPost("Activos")]
    public Task<IActionResult> CrearActivo([FromBody] JsonElement body, CancellationToken cancellationToken) =>
        ForwardAsync("ActivosService", HttpMethod.Post, "Activos", body, cancellationToken);

    [HttpGet("Reportes/depreciacion/activo/{activoId:int}")]
    public Task<IActionResult> ObtenerReporte(int activoId, CancellationToken cancellationToken) =>
        ForwardAsync("ReportesService", HttpMethod.Get, $"Reportes/depreciacion/activo/{activoId}", null, cancellationToken);

    [HttpGet("Reportes/depreciacion/activo/{activoId:int}/pdf")]
    public Task<IActionResult> DescargarReporte(int activoId, CancellationToken cancellationToken) =>
        ForwardFileAsync("ReportesService", $"Reportes/depreciacion/activo/{activoId}/pdf", cancellationToken);

    [HttpGet("Reportes/depreciacion/{anio:int}")]
    public Task<IActionResult> ObtenerReporteAnual(int anio, CancellationToken cancellationToken) =>
        ForwardAsync("ReportesService", HttpMethod.Get, $"Reportes/depreciacion/{anio}", null, cancellationToken);

    private async Task<IActionResult> ForwardAsync(
        string clientName,
        HttpMethod method,
        string path,
        JsonElement? body,
        CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient(clientName);
        using var request = new HttpRequestMessage(method, path);

        if (body.HasValue)
            request.Content = JsonContent.Create(body.Value);

        if (Request.Headers.TryGetValue("Authorization", out var authorization))
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(authorization.ToString());

        using var response = await client.SendAsync(request, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/json";
        return new ContentResult
        {
            StatusCode = (int)response.StatusCode,
            Content = content,
            ContentType = contentType
        };
    }

    private async Task<IActionResult> ForwardFileAsync(string clientName, string path, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient(clientName);
        using var request = new HttpRequestMessage(HttpMethod.Get, path);

        if (Request.Headers.TryGetValue("Authorization", out var authorization))
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(authorization.ToString());

        using var response = await client.SendAsync(request, cancellationToken);
        var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
            return new ContentResult
            {
                StatusCode = (int)response.StatusCode,
                Content = Encoding.UTF8.GetString(content),
                ContentType = response.Content.Headers.ContentType?.ToString() ?? "application/json"
            };

        return File(content, "application/pdf", $"reporte-{DateTime.UtcNow:yyyyMMddHHmmss}.pdf");
    }
}
