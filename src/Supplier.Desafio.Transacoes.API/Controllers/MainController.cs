using Microsoft.AspNetCore.Mvc;
using Supplier.Desafio.Transacoes.Aplicacao.Core.Notificacoes;

namespace Supplier.Desafio.Transacoes.API.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotificador notificador;
    public MainController(INotificador notificador)
    {
        this.notificador = notificador;
    }

    protected bool OperacaoValida()
    {
        return !notificador.TemNotificacao();
    }

    protected ActionResult CustomResponse(object result = null)
    {
        if (OperacaoValida())
        {
            return Ok(new
            {
                sucesso = true,
                resultado = result
            });
        }

        return BadRequest(new
        {
            sucesso = false,
            erros = notificador.ObterNotificacoes().Select(n => n.Mensagem),
            resultado = result
        });
    }
}