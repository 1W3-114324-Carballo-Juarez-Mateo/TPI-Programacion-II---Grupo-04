using Microsoft.EntityFrameworkCore;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Implementations;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;
using WebApi_TPI_AIRCNR_PII.Services.Implementations;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AlquileresContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositorios
builder.Services.AddScoped<IAlquilerRepository, AlquilerRepository>();
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
builder.Services.AddScoped<IAuxiliarRepository<Marca>, MarcasRepository>();
builder.Services.AddScoped<IAuxiliarRepository<Tipos_Vehiculo>, TiposVehiculosRepository>();
builder.Services.AddScoped<IAuxiliarRepository<Tipos_Documento>, TipoDocsRepository>();
builder.Services.AddScoped<IAuxiliarRepository<Estados_Vehiculo>, EstadoVehiculoRepository>();
builder.Services.AddScoped<IAuxiliarRepository<Sucursal>, SucursalRepository>();

//Services
builder.Services.AddScoped<IAlquilerService, AlquilerService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IAuxiliarService, MarcasService>();
builder.Services.AddScoped<IAuxiliarService, TiposVehiculoService>();
builder.Services.AddScoped<IAuxiliarService, TipoDocsService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Activar CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
