using Dapper;
using MySqlX.XDevAPI;
using Supplier.Desafio.Clientes.Infra;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Entidades;

namespace Supplier.Desafio.Transacoes.Infra.Transacoes.Repositorios;

public class TransacoesRepositorio : ITransacoesRepositorio
{
    private readonly DatabaseConnection _databaseConnection;

    public TransacoesRepositorio(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public async Task<int> InserirAsync(Transacao transacao)
    {
        using (var connection = _databaseConnection.CreateConnection())
        {
            var query = @"
                INSERT INTO transacoes (IdTransacao, IdCliente, ValorSimulacao, Status)
                VALUES (@IdTransacao, @IdCliente, @ValorSimulacao, @Status);
                SELECT LAST_INSERT_ID();
                ";

            int id = await connection.ExecuteScalarAsync<int>(query, new
            {
                IdTransacao = transacao.IdTransacao,
                IdCliente = transacao.IdCliente,
                ValorSimulacao = transacao.ValorSimulacao,
                Status = transacao.Status
            });

            return id;
        }
    }
}
