using Application.Contracts.Persistence;
using Infrastructure.Persistence.DatabaseContext;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection; // Necesario para MediatR

// --- 1. Crear el constructor de la aplicación ---
var builder = WebApplication.CreateBuilder(args);


// --- 2. Configurar los servicios en el contenedor de Inyección de Dependencias ---

// Añadimos el servicio de controladores para que la API sepa cómo manejarlos.
builder.Services.AddControllers();

// Añadimos el DbContext al contenedor.
builder.Services.AddDbContext<PlataformaCursosDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PlataformaCursosConnection");
    options.UseNpgsql(connectionString);
});

// Registramos el UnitOfWork. Cuando se pida un IUnitOfWork, se creará una instancia de UnitOfWork.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registramos MediatR, buscando los handlers en el proyecto Application.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));

// Añadimos los servicios de Swagger para la documentación interactiva.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- 3. Construir la aplicación ---
var app = builder.Build();


// --- 4. Configurar el pipeline de peticiones HTTP ---

// Usamos Swagger y SwaggerUI siempre para facilitar el desarrollo.
app.UseSwagger();
app.UseSwaggerUI();

// Comentamos la redirección a HTTPS para evitar problemas en local.
// app.UseHttpsRedirection();

// Habilitamos la autorización (aunque no la usemos todavía).
app.UseAuthorization();

// Le decimos a la aplicación que use los endpoints definidos en nuestros controladores.
app.MapControllers();


// --- 5. Ejecutar la aplicación ---
app.Run();