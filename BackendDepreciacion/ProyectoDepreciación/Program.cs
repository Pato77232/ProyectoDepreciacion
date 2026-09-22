using Microsoft.EntityFrameworkCore;
using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Application.UseCases;
using ProyectoDepreciación.Infrastructure.Data;

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

var app = builder.Build();

// 5. Configurar Pipeline de Peticiones en Desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();