using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using DoctorsApi.Models;
using DoctorsApi.Transactions;

namespace DoctorsApi.Helpers
{
    public class RabbitReceiver
    {
        public void NewUserReceiver()
        {
            var mres = new ManualResetEventSlim(false);
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = ConnectionFactory.DefaultUser, Password = ConnectionFactory.DefaultPass, Port = AmqpTcpEndpoint.UseDefaultPort };
            DoctorModel user = new DoctorModel();
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "NewDoctor", type: ExchangeType.Fanout);

                    var queueName = channel.QueueDeclare().QueueName;

                    channel.QueueBind(queue: queueName, exchange: "NewDoctor", routingKey: "");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var utf8_message = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(utf8_message);
                        user = (DoctorModel)utf8_message.Deserialize(typeof(DoctorModel))!;
                        Registries insertUser = new Registries();
                        insertUser.UpdateDoctorsProfile(user);
                    };

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    mres.Wait();
                }
            }
        }

        public void DeleteUserReceiver()
        {
            var mres = new ManualResetEventSlim(false);
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = ConnectionFactory.DefaultUser, Password = ConnectionFactory.DefaultPass, Port = AmqpTcpEndpoint.UseDefaultPort };
            DoctorModel user = new DoctorModel();
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
                        user = (DoctorModel)utf8_message.Deserialize(typeof(DoctorModel))!;
                        Registries insertUser = new Registries();
                        insertUser.DeleteDoctorsProfile(user);
                    };

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    mres.Wait();
                }
            }
        }
    }
}
