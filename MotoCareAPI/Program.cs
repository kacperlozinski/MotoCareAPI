using Microsoft.EntityFrameworkCore;
using MotoCareAPI.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddDbContext<MotoCareDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=MotoCare;User Id=sa;Password=Kacper123;TrustServerCertificate=True;"));
*/
var app = builder.Build();

// Middleware
app.UseHttpsRedirection();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MotoCareAPI v1");
        c.RoutePrefix = "swagger"; 
    });
}


app.MapControllers();

app.Run();
