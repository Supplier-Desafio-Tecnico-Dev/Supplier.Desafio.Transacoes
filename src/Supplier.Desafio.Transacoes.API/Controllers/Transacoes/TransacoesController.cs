using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supplier.Desafio.Commons.Notificacoes;
using Supplier.Desafio.Transacoes.Aplicacao.Transacoes.Servicos.Interfaces;
using Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Requests;
using Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Responses;

namespace Supplier.Desafio.Transacoes.API.Controllers.Transacoes;

[Route("api/transacoes")]
[Authorize]
public class TransacoesController : MainController
{
    private readonly ITransacoesAppServico _transacoesAppServico;

    public TransacoesController(ITransacoesAppServico transacoesAppServico, INotificador notificador) : base(notificador)
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
