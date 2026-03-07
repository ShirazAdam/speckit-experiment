var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// configure simple CORS policy for development
var allowOrigins = new[] { "http://localhost:5173" };
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy.WithOrigins(allowOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// register in-memory repository (use interface for SOLID)
builder.Services.AddSingleton<ToDoApi.Interfaces.ITodoRepository, ToDoApi.Repositories.InMemoryTodoRepository>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("DevCors");

app.MapControllers();

app.Run();

