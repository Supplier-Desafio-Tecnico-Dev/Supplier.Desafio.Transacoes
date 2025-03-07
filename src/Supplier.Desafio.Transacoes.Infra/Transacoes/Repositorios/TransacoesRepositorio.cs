using Dapper;
using Supplier.Desafio.Commons.Data;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Entidades;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Repositorios;

namespace Supplier.Desafio.Transacoes.Infra.Transacoes.Repositorios;

public class TransacoesRepositorio : RepositorioDapper<Transacao>, ITransacoesRepositorio
{
    public TransacoesRepositorio(DapperDbContext dapperContext) : base(dapperContext) { }

    public async Task<Transacao?> ObterPorIdAsync(int id)
    {
        var query = @"
        SELECT Id, IdTransacao, IdCliente, ValorSimulacao, Status
        FROM transacoes
        WHERE Id = @Id;";

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await session.QuerySingleOrDefaultAsync<Transacao>(query, parameters);
    }

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

    public async Task AtualizarAsync(Transacao transacao)
    {
        var query = @"
        UPDATE transacoes
        SET ValorSimulacao = @ValorSimulacao,
            Status = @Status
        WHERE Id = @Id;";

        var parameters = new DynamicParameters();
        parameters.Add("@Id", transacao.Id);
        parameters.Add("@ValorSimulacao", transacao.ValorSimulacao);
        parameters.Add("@Status", transacao.Status);

        await session.ExecuteAsync(query, parameters);
    }
}
