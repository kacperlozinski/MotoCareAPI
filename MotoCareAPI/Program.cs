var builder = WebApplication.CreateBuilder(args);

// Dodaj us�ug� kontroler�w
builder.Services.AddControllers();

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();

// Mapowanie kontroler�w
app.MapControllers();

app.Run();
