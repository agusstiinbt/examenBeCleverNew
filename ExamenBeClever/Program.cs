using ExamenBeClever.Models;
using ExamenBeClever.Servicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Conexion con BBDD
builder.Services.AddDbContext<BeCleverContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionString")));

//Inyecciones de dependencia
builder.Services.AddTransient<IRegistro, RegistroService>();
builder.Services.AddTransient<IEmpleado, EmpleadoService>();


//Mapeos
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


//Creamos un ámbito
using(var scope = app.Services.CreateScope())
{
    var context=scope.ServiceProvider.GetRequiredService<BeCleverContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
