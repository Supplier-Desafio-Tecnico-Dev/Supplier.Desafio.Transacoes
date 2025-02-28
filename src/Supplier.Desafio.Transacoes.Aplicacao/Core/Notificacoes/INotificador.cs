namespace Supplier.Desafio.Transacoes.Aplicacao.Core.Notificacoes;

public interface INotificador
{
    bool TemNotificacao();
    IEnumerable<Notificacao> ObterNotificacoes();
    void Handle(Notificacao notificacao);
}