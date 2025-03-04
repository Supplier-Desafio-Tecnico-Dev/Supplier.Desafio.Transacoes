using Supplier.Desafio.Commons.Data;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Entidades;

namespace Supplier.Desafio.Transacoes.Dominio.Transacoes.Repositorios;

public interface ITransacoesRepositorio : IRepositorioDapper<Transacao>
{
    Task<int> InserirAsync(Transacao transacao);
}
