using FluentValidation;
using FluentValidation.Results;
using Supplier.Desafio.Transacoes.Aplicacao.Core.Notificacoes;

namespace Supplier.Desafio.Clientes.Aplicacao.Core;

public abstract class ServicoBase
{
    private readonly INotificador _notificador;

    protected ServicoBase(INotificador notificador)
    {
        _notificador = notificador;
    }

    protected void Notificar(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Notificar(error.ErrorMessage);
        }
    }

    protected void Notificar(string mensagem)
    {
        _notificador.Handle(new Notificacao(mensagem));
    }

    protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : class
    {
        var validator = validacao.Validate(entidade);

        if (validator.IsValid) return true;

        Notificar(validator);

        return false;
    }
}