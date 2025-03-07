using Supplier.Desafio.Transacoes.Dominio.Transacoes.Entidades;

namespace Supplier.Desafio.Transacoes.Dominio.Transacoes.Servicos.Interfaces
{
    public interface ITransacoesServico
    {
        Task AtualizarStatusAsync(Transacao transacao, string status);
        Task<Transacao> ValidarAsync(int id);
    }
}