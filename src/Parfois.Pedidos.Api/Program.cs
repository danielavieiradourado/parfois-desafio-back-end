using Microsoft.EntityFrameworkCore;
using Parfois.Pedidos.Application.Interfaces;
using Parfois.Pedidos.Application.Servicos;
using Parfois.Pedidos.Infrastructure;
using Parfois.Pedidos.Infrastructure.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Base de dados em memória
builder.Services.AddDbContext<PedidosDbContext>(options =>
    options.UseInMemoryDatabase("PedidosDb"));


// Repositórios
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

// Serviços
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IAprovacaoService, AprovacaoService>();
builder.Services.AddSingleton<IAvaliadorEstado, AvaliadorEstado>();

var app = builder.Build();

app.MapOpenApi();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Parfois Pedidos API");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Torna a classe Program acessível aos testes de integração
public partial class Program { }
