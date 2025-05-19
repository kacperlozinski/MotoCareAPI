using Microsoft.EntityFrameworkCore;
using MotoCareAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Dodaj us³ugê kontrolerów
builder.Services.AddControllers();

// Dodaj us³ugê Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MotoCareDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=MotoCare;User Id=sa;Password=Kacper123;TrustServerCertificate=True;"));

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();

// Middleware Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MotoCareAPI v1");
        c.RoutePrefix = "swagger"; 
    });
}

// Mapowanie kontrolerów
app.MapControllers();

app.Run();
