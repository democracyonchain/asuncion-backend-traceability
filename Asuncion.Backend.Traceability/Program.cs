using Asuncion.Backend.Traceability.Repositories;
using Asuncion.Backend.Traceability.Services;
using Asuncion.Backend.Traceability.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger UI (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("dev", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddScoped<CatalogoRepository>();
builder.Services.AddScoped<CatalogoService>();

builder.Services.AddScoped<TraceabilityRepository>();
builder.Services.AddScoped<TraceabilityService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // JSON en /swagger/v1/swagger.json
    app.UseSwaggerUI(); // UI en /swagger
}

app.UseHttpsRedirection();
app.UseCors("dev");

app.MapControllers();

app.Run();
