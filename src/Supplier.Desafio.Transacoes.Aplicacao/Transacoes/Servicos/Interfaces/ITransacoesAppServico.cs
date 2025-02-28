using Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Requests;
using Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Responses;

namespace Supplier.Desafio.Transacoes.Aplicacao.Transacoes.Servicos.Interfaces
{
    public interface ITransacoesAppServico
    {
        Task<TransacaoNovaResponse> InserirAsync(TransacaoNovaRequest request);
    }
}