using FluentValidation;

namespace Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Requests.Validadores;

public class TransacaoNovaRequestValidador : AbstractValidator<TransacaoNovaRequest>
{
    public TransacaoNovaRequestValidador()
    {
        RuleFor(x => x.IdCliente)
            .GreaterThan(0)
            .WithMessage("IdCliente deve ser maior que 0");

        RuleFor(x => x.ValorSimulacao)
            .GreaterThan(0)
            .WithMessage("ValorSimulacao deve ser maior que 0");
    }
}
