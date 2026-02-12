using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Mi API", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new()
    {
        Description = "Clave de API en el header: X-API-KEY",
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new() { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<GestionDeAviones.DA.DBContexto>(options =>
    options.UseInMemoryDatabase("AvionesDB"));

builder.Services.AddScoped<GestionDeAviones.BL.IAvionRepository, GestionDeAviones.DA.AvionRepository>();
builder.Services.AddScoped<GestionDeAviones.BL.IAdministradorDeAviones, GestionDeAviones.BL.AdministradorDeAviones>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
