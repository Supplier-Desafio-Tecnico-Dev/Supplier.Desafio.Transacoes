namespace Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Responses
{
    public record TransacaoNovaResponse(Guid IdTransacao,
                                        string Status,
                                        IReadOnlyList<string> DetalheErro);
}
