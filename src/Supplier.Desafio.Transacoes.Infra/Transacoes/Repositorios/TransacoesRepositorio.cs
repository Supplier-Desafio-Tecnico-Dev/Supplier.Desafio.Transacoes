using Dapper;
using Supplier.Desafio.Commons.Data;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Entidades;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Repositorios;

namespace Supplier.Desafio.Transacoes.Infra.Transacoes.Repositorios;

public class TransacoesRepositorio : RepositorioDapper<Transacao>, ITransacoesRepositorio
{
    public TransacoesRepositorio(DapperDbContext dapperContext) : base(dapperContext) { }

    public async Task<int> InserirAsync(Transacao transacao)
    {
        var query = @"
                INSERT INTO transacoes (IdTransacao, IdCliente, ValorSimulacao, Status)
                VALUES (@IdTransacao, @IdCliente, @ValorSimulacao, @Status);
                SELECT LAST_INSERT_ID();
                ";

        var parameters = new DynamicParameters();
        parameters.Add("@IdTransacao", transacao.IdTransacao);
        parameters.Add("@IdCliente", transacao.IdCliente);
        parameters.Add("@ValorSimulacao", transacao.ValorSimulacao);
        parameters.Add("@Status", transacao.Status);

        return await session.ExecuteScalarAsync<int>(query, parameters);
    }
}
