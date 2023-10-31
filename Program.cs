using primeraApi.Modelos.Datos;
using Microsoft.EntityFrameworkCore;
using primeraApi;
using primeraApi.Repositorio.IRepositorio;
using primeraApi.Repositorio;

var builder = WebApplication.CreateBuilder(args);

//add-migration
//update-database

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<INumeroVillaRepositorio, NumeroVillaRepositorio>();
builder.Services.AddScoped<IVillaRepositorio, VillaRepositorio>();
builder.Services.AddSqlServer<AplicationDbContext>(builder.Configuration.GetConnectionString("labasedatos"), builder =>
{
    builder.EnableRetryOnFailure();
});
builder.Services.AddAutoMapper(typeof(MappingConfig));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
