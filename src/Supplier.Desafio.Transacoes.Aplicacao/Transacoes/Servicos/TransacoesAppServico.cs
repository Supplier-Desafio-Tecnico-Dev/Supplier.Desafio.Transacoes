using Supplier.Desafio.Commons;
using Supplier.Desafio.Commons.Notificacoes;
using Supplier.Desafio.Transacoes.Aplicacao.Transacoes.Servicos.Interfaces;
using Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Requests;
using Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Requests.Validadores;
using Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Responses;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Entidades;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Enums;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Repositorios;

namespace Supplier.Desafio.Transacoes.Aplicacao.Transacoes.Servicos
{
    public class TransacoesAppServico : ServicoBase, ITransacoesAppServico
    {
        private readonly INotificador _notificador;
        private readonly ITransacoesRepositorio _transacoesRepositorio;

        public TransacoesAppServico(INotificador notificador,
                                    ITransacoesRepositorio transacoesRepositorio) : base(notificador)
        {
            _notificador = notificador;
            _transacoesRepositorio = transacoesRepositorio;
        }

        public async Task<TransacaoNovaResponse> InserirAsync(TransacaoNovaRequest request)
        {
            if (!ExecutarValidacao(new TransacaoNovaRequestValidador(), request))
                return new TransacaoNovaResponse(Guid.Empty, StatusTransacaoEnum.Erro.ToString(), _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList());

            //Obter Cliente

            var transacao = new Transacao(request.IdCliente, request.ValorSimulacao, StatusTransacaoEnum.Aprovado.ToString());

            var id = await _transacoesRepositorio.InserirAsync(transacao);
            if (id <= 0)
            {
                Notificar("Erro ao inserir cliente");
                return new TransacaoNovaResponse(Guid.Empty, StatusTransacaoEnum.Erro.ToString(), _notificador.ObterNotificacoes().Select(c => c.Mensagem).ToList());
            }

            // await _rabbitMQPublisher.PublishMessageAsync(transacao, "cliente_debitarlimite");

            return new TransacaoNovaResponse(transacao.IdTransacao, transacao.Status, []);
        }
    }
}
