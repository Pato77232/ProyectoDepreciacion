using Microsoft.EntityFrameworkCore;
using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Application.UseCases;
using ProyectoDepreciación.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar Controladores
builder.Services.AddControllers();

// 2. Configurar Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Configurar Base de Datos SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4. Inyección de Dependencias (Repositores y Casos de Uso)
builder.Services.AddScoped<IActivoRepository, ActivoRepository>();
builder.Services.AddScoped<RegistrarActivo>();
builder.Services.AddScoped<ObtenerActivo>();
builder.Services.AddScoped<ListarActivos>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
// 5. Configurar Pipeline de Peticiones en Desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.MapControllers();

app.Run();