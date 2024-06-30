using UserApi.Models;
using UserApi.Transactions;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace UserApi.Helpers
{
    public class RabbitReceiver
    {
        public void NewUserReceiver()
        {
            var mres = new ManualResetEventSlim(false);
            var factory = new ConnectionFactory() { HostName = "localhost" };
            UserModel user = new UserModel();
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
                        user = (UserModel)utf8_message.Deserialize(typeof(UserModel))!;
                        Registries insertUser = new Registries();
                        insertUser.InsertDBUser(user);
                    };

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    mres.Wait();
                }
            }
        }
    }
}
