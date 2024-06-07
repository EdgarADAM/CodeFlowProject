// See https://aka.ms/new-console-template for more information
using Common.Models;
using Common.Helpers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        channel.ExchangeDeclare(exchange: "UserRegistries", type: ExchangeType.Fanout);

        var queueName = channel.QueueDeclare().QueueName;

        channel.QueueBind(queue: queueName, exchange: "UserRegistries", routingKey: "");

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var utf8_message = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(utf8_message);
            UserModel user = (UserModel)utf8_message.Deserialize(typeof(UserModel))!;

            Console.WriteLine(user.name);
            Console.WriteLine(user.type);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        Console.WriteLine("espera");
        Console.ReadLine();
    }
}
