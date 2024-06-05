using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RedisPrototype.Infrastructure;
using RedisPrototype.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configurationManager = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RedisDBContext>(options => options.UseSqlServer(configurationManager.GetConnectionString("ConnStr")));
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = configurationManager["RedisCacheUrl"]; });

//builder.Services.AddSingleton<IDBService, DBService>();
builder.Services.AddScoped<IDBService, DBService>();
//builder.Services.AddSingleton<IDBService>("DBService", new DBService());

var app = builder.Build();

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
