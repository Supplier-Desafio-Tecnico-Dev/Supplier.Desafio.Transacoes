using MySqlX.XDevAPI;
using Supplier.Desafio.Commons.Dominio.Exceptions;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Entidades;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Repositorios;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Servicos.Interfaces;

namespace Supplier.Desafio.Transacoes.Dominio.Transacoes.Servicos
{
    public class TransacoesServico : ITransacoesServico
    {
        private readonly ITransacoesRepositorio _transacoesRepositorio;

        public TransacoesServico(ITransacoesRepositorio transacoesRepositorio)
        {
            _transacoesRepositorio = transacoesRepositorio;
        }

        public async Task<Transacao> ValidarAsync(int id)
        {
            var transacao = await _transacoesRepositorio.ObterPorIdAsync(id);

            return transacao ?? throw new DominioException("Transação não encontrada");
        }

        public async Task AtualizarStatusAsync(Transacao transacao, string status)
        {
            transacao.SetStatus(status);
            await _transacoesRepositorio.AtualizarAsync(transacao);
        }
    }
}
