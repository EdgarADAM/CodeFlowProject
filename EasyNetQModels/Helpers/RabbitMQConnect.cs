using RabbitMQ.Client;
using Common.Models;

namespace Common.Helpers
{
    public class RabbitMQConnect
    {
       public void SenderRabbit(object sender, string topic)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var message = Serializer.Serialize(sender);
                   
                    channel.ExchangeDeclare(exchange: topic, type: ExchangeType.Fanout);
                    channel.BasicPublish(exchange: topic, routingKey: "", basicProperties: null, body: message);
                }
            }
        }
    }
}
