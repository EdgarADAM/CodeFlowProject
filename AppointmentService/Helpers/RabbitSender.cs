using RabbitMQ.Client;
using AppointmentService.Models;
using AppointmentService.Helpers;

namespace AppointmentService.Helpers
{
    public class RabbitSender
    {
        public void MessageRabbit(string emailAdress, string subjectMessage, string bodyMessage)
        {
            EmailModel emailMessage = new EmailModel();
            emailMessage.Email = emailAdress;
            emailMessage.Name = "Medical Service Premium";
            emailMessage.Subject = subjectMessage;
            emailMessage.Body = bodyMessage;

            var factory = new ConnectionFactory() { HostName = "localhost", UserName = ConnectionFactory.DefaultUser, Password = ConnectionFactory.DefaultPass, Port = AmqpTcpEndpoint.UseDefaultPort };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var message = Serializer.Serialize(emailMessage);

                    channel.ExchangeDeclare(exchange: "EmailMessages", type: ExchangeType.Fanout);
                    channel.BasicPublish(exchange: "EmailMessages", routingKey: "", basicProperties: null, body: message);
                }
            }
        }
    }
}
