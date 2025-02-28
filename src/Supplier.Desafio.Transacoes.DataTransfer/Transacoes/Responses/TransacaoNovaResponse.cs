namespace Supplier.Desafio.Transacoes.DataTransfer.Transacoes.Responses
{
    public class TransacaoNovaResponse
    {
        public Guid IdTransacao { get; set; }
        public string Status { get; set; }
        public IReadOnlyList<string> DetalheErro { get; set; }
    }
}
