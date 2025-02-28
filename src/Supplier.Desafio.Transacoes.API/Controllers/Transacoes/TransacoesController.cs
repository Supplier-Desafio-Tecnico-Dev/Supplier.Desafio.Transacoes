using Microsoft.AspNetCore.Mvc;
using Supplier.Desafio.Transacoes.Aplicacao.Core.Notificacoes;
using Supplier.Desafio.Transacoes.Aplicacao.Transacoes.Servicos.Interfaces;
using Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Requests;
using Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Responses;

namespace Supplier.Desafio.Transacoes.API.Controllers.Transacoes
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacoesController : MainController
    {
        private readonly ITransacoesAppServico _transacoesAppServico;

        public TransacoesController(INotificador notificador,
                                    ITransacoesAppServico transacoesAppServico) : base(notificador)
        {
            _transacoesAppServico = transacoesAppServico;
        }

        [HttpPost]
        public async Task<ActionResult<TransacaoNovaResponse>> InserirAsync(TransacaoNovaRequest request)
        {
            var response = await _transacoesAppServico.InserirAsync(request);

            return CustomResponse(response);
        }
    }
}
