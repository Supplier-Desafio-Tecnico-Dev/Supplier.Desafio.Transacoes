using Supplier.Desafio.Transacoes.Dominio.Transacoes.Entidades;

namespace Supplier.Desafio.Transacoes.Infra.Transacoes.Repositorios
{
    public interface ITransacoesRepositorio
    {
        Task<int> InserirAsync(Transacao transacao);
    }
}