using Microsoft.Extensions.Hosting;
using Supplier.Desafio.Commons.MessageBus;
using Supplier.Desafio.Commons.MessageBus.Events;
using Supplier.Desafio.Transacoes.Dominio.Transacoes.Servicos.Interfaces;

namespace Supplier.Desafio.Transacoes.Consumer.Handlers;

public class ResultadoTransacaoHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly ITransacoesServico _transacoesServico;

    public ResultadoTransacaoHandler(IMessageBus bus, ITransacoesServico transacoesServico)
    {
        _bus = bus;
        _transacoesServico = transacoesServico;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetSubscribers();
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private void SetSubscribers()
    {
        _bus.SubscribeAsync<ResultadoTransacaoEvent>("ResultadoTransacao", async request =>
            await SetarResultadoTransacaoAsync(request));
    }

    private async Task SetarResultadoTransacaoAsync(ResultadoTransacaoEvent mensagem)
    {
        Console.WriteLine($"Resultado da transação {mensagem.IdTransacao} - {mensagem.StatusTransacao}");

        var transacao = await _transacoesServico.ValidarAsync(mensagem.IdTransacao);

        await _transacoesServico.AtualizarStatusAsync(transacao, mensagem.StatusTransacao);
    }
}
