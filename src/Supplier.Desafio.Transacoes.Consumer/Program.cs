using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Supplier.Desafio.Commons.Data;
using Supplier.Desafio.Transacoes.Consumer;
using Supplier.Desafio.Transacoes.Consumer.Handlers;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Repositorios;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Servicos;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Servicos.Interfaces;
using Supplier.Desafio.Transacoes.Infra.Transacoes.Repositorios;

var builder = Host.CreateApplicationBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddSingleton(new DapperDbContext("Server=172.30.94.105;Port=3307;Database=supplier_desafio;Uid=usuario;Pwd=senha;"));

builder.Services.AddScoped<ITransacoesServico, TransacoesServico>();
builder.Services.AddScoped<ITransacoesRepositorio, TransacoesRepositorio>();

builder.Services.AddMessageBus("host=localhost:5672;timeout=10")
    .AddHostedService<ResultadoTransacaoHandler>();

var host = builder.Build();
host.Run();
