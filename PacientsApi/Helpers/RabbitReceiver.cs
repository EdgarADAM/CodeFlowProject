using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using PacientsApi.Models;
using Pacients.Transactions;
using PacientsApi.Helpers;

namespace Pacients.Helpers
{
    public class RabbitReceiver
    {
        public void NewUserReceiver()
        {
            var mres = new ManualResetEventSlim(false);
            var factory = new ConnectionFactory() { HostName = "localhost" };
            PacientModel user = new PacientModel();
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "NewPacient", type: ExchangeType.Fanout);

                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue: queueName, exchange: "NewPacient", routingKey: "");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var utf8_message = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(utf8_message);
                        user = (PacientModel)utf8_message.Deserialize(typeof(PacientModel))!;
                        Registries insertUser = new Registries();
                        insertUser.UpdatePacientsProfile(user);
                    };

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    mres.Wait();
                }
            }
        }

        public void DeleteUserReceiver()
        {
            var mres = new ManualResetEventSlim(false);
            var factory = new ConnectionFactory() { HostName = "localhost" };
            PacientModel user = new PacientModel();
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "DeletePacient", type: ExchangeType.Fanout);

                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue: queueName, exchange: "DeletePacient", routingKey: "");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var utf8_message = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(utf8_message);
                        user = (PacientModel)utf8_message.Deserialize(typeof(PacientModel))!;
                        Registries insertUser = new Registries();
                        insertUser.DeletePacientsProfile(user);
                    };

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    mres.Wait();
                }
            }
        }
    }
}
