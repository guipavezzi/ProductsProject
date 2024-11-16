using Microsoft.EntityFrameworkCore;
using ProductProject.Application.Interfaces;
using ProductProject.Application.Services;
using ProductProject.Domain.Interfaces;
using ProductProject.Infrastructure.Persistence.Context;
using ProductProject.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Products API", Version = "v1" });
});
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

// Lendo variáveis de ambiente do Railway
var host = Environment.GetEnvironmentVariable("HOST");
var port = Environment.GetEnvironmentVariable("PORT");
var database = Environment.GetEnvironmentVariable("DATABASE");
var username = Environment.GetEnvironmentVariable("USERNAME");
var password = Environment.GetEnvironmentVariable("PASSWORD");


Console.WriteLine("Host: " + host);
Console.WriteLine("port: " + port);
Console.WriteLine("database: " + database);
Console.WriteLine("username: " + username);
Console.WriteLine("password: " + password);

// Construindo a string de conexão
if (!string.IsNullOrEmpty(host) &&
    !string.IsNullOrEmpty(port) &&
    !string.IsNullOrEmpty(database) &&
    !string.IsNullOrEmpty(username) &&
    !string.IsNullOrEmpty(password))
{
    var connectionString = $"Host={host};" +
                           $"Port={port};" +
                           $"Database={database};" +
                           $"Username={username};" +
                           $"Password={password};" +
                           "SSL Mode=Require;Trust Server Certificate=true";

    builder.Services.AddDbContext<ProductContext>(opt => opt.UseNpgsql(connectionString));
}
else
{
    throw new Exception("As variáveis de ambiente necessárias não estão configuradas.");
}

// Registrando serviços
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configuração do Swagger
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
