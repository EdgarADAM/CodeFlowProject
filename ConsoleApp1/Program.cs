using Consola.Models;
using RabbitMQ.Client;
using Common.Helpers;


class Sender()
{
    public static void Main(string []args)
    {
        UserModel usuario = new UserModel();
        usuario.name = "Edgar";
        usuario.userId = "Chino";
        usuario.type = "2";

        var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var message = Serializer.Serialize(usuario);

                    channel.ExchangeDeclare(exchange: "UserRegistries", type: ExchangeType.Fanout);
                    channel.BasicPublish(exchange: "UserRegistries", routingKey: "", basicProperties: null, body: message);
                }
            }
    }
}