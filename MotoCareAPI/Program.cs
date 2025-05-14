var builder = WebApplication.CreateBuilder(args);

// Dodaj us³ugê kontrolerów
builder.Services.AddControllers();

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();

// Mapowanie kontrolerów
app.MapControllers();

app.Run();
