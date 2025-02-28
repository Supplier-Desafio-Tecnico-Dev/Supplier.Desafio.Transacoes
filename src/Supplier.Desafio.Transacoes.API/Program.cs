using Supplier.Desafio.Clientes.Infra;
using Supplier.Desafio.Transacoes.Aplicacao.Core.Notificacoes;
using Supplier.Desafio.Transacoes.Aplicacao.Transacoes.Servicos;
using Supplier.Desafio.Transacoes.Aplicacao.Transacoes.Servicos.Interfaces;
using Supplier.Desafio.Transacoes.Infra.Transacoes.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddSingleton(new DatabaseConnection(connectionString));
builder.Services.AddScoped<ITransacoesRepositorio, TransacoesRepositorio>();

builder.Services.AddScoped<INotificador, Notificador>();
builder.Services.AddScoped<ITransacoesAppServico, TransacoesAppServico>();

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
