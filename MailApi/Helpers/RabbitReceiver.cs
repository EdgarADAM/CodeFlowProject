using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using MailApi.Models;
using MailApi.Helpers;

namespace MailApi.Helpers
{
    public class RabbitReceiver
    {
        public void NewMessageReceiver()
        {
            var mres = new ManualResetEventSlim(false);
            var factory = new ConnectionFactory() { HostName = "localhost" };
            //UserModel user = new UserModel();
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "EmailMessages", type: ExchangeType.Fanout);

                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue: queueName, exchange: "EmailMessages", routingKey: "");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var utf8_message = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(utf8_message);
                        var email = (EmailModel)utf8_message.Deserialize(typeof(EmailModel))!;
                        EmailSender emailMessage = new EmailSender();
                        emailMessage.Email(email);
                    };

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    mres.Wait();
                }
            }
        }
    }
}
