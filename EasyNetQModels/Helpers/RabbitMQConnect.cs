using RabbitMQ.Client;
using Common.Models;

namespace Common.Helpers
{
    public class RabbitMQConnect
    {
       public void NewUser(UserModel newUser)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var message = Serializer.Serialize(newUser);
                   
                    channel.ExchangeDeclare(exchange: "UserRegistries", type: ExchangeType.Fanout);
                    channel.BasicPublish(exchange: "UserRegistries", routingKey: "", basicProperties: null, body: message);
                }
            }
        }
    }
}
