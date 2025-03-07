using Supplier.Desafio.Commons.Data;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Entidades;

namespace Supplier.Desafio.Transacoes.Dominio.Transacoes.Repositorios;

public interface ITransacoesRepositorio : IRepositorioDapper<Transacao>
{
    Task AtualizarAsync(Transacao transacao);
    Task<int> InserirAsync(Transacao transacao);
    Task<Transacao?> ObterPorIdAsync(int id);
}
