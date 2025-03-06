using Supplier.Desafio.Commons.Data;
using Supplier.Desafio.Commons.MessageBus;
using Supplier.Desafio.Commons.Middlewares;
using Supplier.Desafio.Commons.Notificacoes;
using Supplier.Desafio.Transacoes.Aplicacao.Transacoes.Servicos;
using Supplier.Desafio.Transacoes.Aplicacao.Transacoes.Servicos.Interfaces;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Repositorios;
using Supplier.Desafio.Transacoes.Infra.Transacoes.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddSingleton(new DapperDbContext(connectionString));

builder.Services.AddScoped<ITransacoesRepositorio, TransacoesRepositorio>();

builder.Services.AddScoped<ITransacoesAppServico, TransacoesAppServico>();

builder.Services.AddScoped<INotificador, Notificador>();

builder.Services.AddSingleton<IMessageBus>(new MessageBus("host=localhost:5672;timeout=10"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExcecaoMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
