using Newtonsoft.Json;
using RabbitMQ.Client;
using Supplier.Desafio.Transacoes.Aplicacao.Core.RabbitMQ.Interfaces;
using System.Text;

namespace Supplier.Desafio.Transacoes.Aplicacao.Core.RabbitMQ
{
    public class RabbitMQPublisher<T> : IRabbitMQPublisher<T>
    {
        public async Task PublishMessageAsync(T message, string queueName)
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "admin"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var messageJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            await Task.Run(() => channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body));
        }
    }
}
