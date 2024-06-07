using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void NewUserReceiver()
        {
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
                    Console.ReadLine();
                }
            }
        }
    }
}
