using Microsoft.EntityFrameworkCore;
using ProductProject.Application.Interfaces;
using ProductProject.Application.Services;
using ProductProject.Domain.Interfaces;
using ProductProject.Infrastructure.Persistence.Context;
using ProductProject.Infrastructure.Persistence.Repositories;
using ProductProject.Shared.helpers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "Bridal Shower API", Version = "v1"});
});


builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
var apiKey = Environment.GetEnvironmentVariable("API_KEY");
if(!string.IsNullOrEmpty(apiKey))
{
    builder.Services.Configure<AppSettings>(opt => {
        opt.Secret = apiKey;
    });
}
else
{
    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
}

if (!string.IsNullOrEmpty(databaseUrl))
{
    var databaseUri = new Uri(databaseUrl);
    var userInfo = databaseUri.UserInfo.Split(':');

    var connectionString = $"Host={databaseUri.Host};" +
                           $"Port={databaseUri.Port};" +
                           $"Database={databaseUri.LocalPath.TrimStart('/')};" +
                           $"Username={userInfo[0]};" +
                           $"Password={userInfo[1]};" +
                           "SSL Mode=Require;Trust Server Certificate=true";

    builder.Services.AddDbContext<ProductContext>(opt => opt.UseNpgsql(connectionString));
}
else
{
    var appSettingsConnectionString = builder.Configuration.GetConnectionString("ProductConnection");
    builder.Services.AddDbContext<ProductContext>(opt =>
        opt.UseNpgsql(appSettingsConnectionString));
}

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bridal Shower API V1");
        c.RoutePrefix = string.Empty;
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
