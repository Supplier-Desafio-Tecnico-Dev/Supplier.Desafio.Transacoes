namespace Supplier.Desafio.Transacoes.Aplicacao.Core.RabbitMQ.Interfaces
{
    public interface IRabbitMQPublisher<T>
    {
        Task PublishMessageAsync(T message, string queueName);
    }
}
