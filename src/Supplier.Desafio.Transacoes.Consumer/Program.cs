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

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddSingleton(new DapperDbContext(connectionString));

builder.Services.AddScoped<ITransacoesServico, TransacoesServico>();
builder.Services.AddScoped<ITransacoesRepositorio, TransacoesRepositorio>();

var messageBusConfig = builder.Configuration.GetSection("MessageBus");
string messageBusConnection = messageBusConfig["Connection"]!;

builder.Services.AddMessageBus(messageBusConnection)
    .AddHostedService<ResultadoTransacaoHandler>();

var host = builder.Build();
host.Run();
