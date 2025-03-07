using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Supplier.Desafio.Commons.Auth;
using Supplier.Desafio.Commons.Data;
using Supplier.Desafio.Commons.MessageBus;
using Supplier.Desafio.Commons.Middlewares;
using Supplier.Desafio.Commons.Notificacoes;
using Supplier.Desafio.Transacoes.Aplicacao.Transacoes.Servicos;
using Supplier.Desafio.Transacoes.Aplicacao.Transacoes.Servicos.Interfaces;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Repositorios;
using Supplier.Desafio.Transacoes.Infra.Transacoes.Repositorios;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Supplier - Transações API",
        Description = "Api de gerenciamento de transações",
        Contact = new OpenApiContact() { Name = "Igor Daflon do Couto", Email = "igordafloncouto@gmail.com" },
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT desta maneira: Bearer {seu token}",
        Name = "Authorization",
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddSingleton(new DapperDbContext(connectionString));

builder.Services.AddScoped<ITransacoesRepositorio, TransacoesRepositorio>();

builder.Services.AddScoped<ITransacoesAppServico, TransacoesAppServico>();

builder.Services.AddScoped<INotificador, Notificador>();

builder.Services.AddSingleton<IMessageBus>(new MessageBus("host=localhost:5672;timeout=10"));

var jwtConfigSection = builder.Configuration.GetSection("JwtConfig");
builder.Services.Configure<JwtConfig>(jwtConfigSection);

var jwtConfig = jwtConfigSection.Get<JwtConfig>();
var key = Encoding.ASCII.GetBytes(jwtConfig!.Segredo);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    bearerOptions.RequireHttpsMetadata = true;
    bearerOptions.SaveToken = true;
    bearerOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = jwtConfig.ValidoEm,
        ValidIssuer = jwtConfig.Emissor
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExcecaoMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
