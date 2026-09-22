using Microsoft.EntityFrameworkCore;
using ProyectoDepreciación.Application.Interfaces;
using ProyectoDepreciación.Application.UseCases;
using ProyectoDepreciación.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();  // <- esto genera el /openapi/v1.json

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IActivoRepository, ActivoRepository>();
builder.Services.AddScoped<RegistrarActivo>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();