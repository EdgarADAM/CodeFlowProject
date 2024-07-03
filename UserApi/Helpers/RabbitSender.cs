using RabbitMQ.Client;
using UserApi.Models;

namespace UserApi.Helpers
{
    public class RabbitSender
    {
        public void MessageRabbit(string emailAdress, string userName, string subjectMessage, string bodyMessage)
        {
            EmailModel emailMessage = new EmailModel();
            emailMessage.Email = emailAdress;
            emailMessage.Name = userName;
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

        public void NewUser(UserModel user)
        {
            if (user.type == "1")
            {
                DoctorModel doctor = new DoctorModel()
                {
                    userId = user.userId,
                    name = user.name,
                    emailAddress = user.emailAddress,
                    birthDate = user.birthDate,
                    country = user.country,
                    phoneNumber = user.phoneNumber
                };
                var factory = new ConnectionFactory() { HostName = "localhost", UserName = ConnectionFactory.DefaultUser, Password = ConnectionFactory.DefaultPass, Port = AmqpTcpEndpoint.UseDefaultPort };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var message = Serializer.Serialize(doctor);

                        channel.ExchangeDeclare(exchange: "NewDoctor", type: ExchangeType.Fanout);
                        channel.BasicPublish(exchange: "NewDoctor", routingKey: "", basicProperties: null, body: message);
                    }
                }
                
            }
            else
            {
                PacientModel pacient = new PacientModel()
                {
                    userId = user.userId,
                    name = user.name,
                    emailAddress = user.emailAddress,
                    birthDate = user.birthDate,
                    country = user.country,
                    phoneNumber = user.phoneNumber
                };
                var factory = new ConnectionFactory() { HostName = "localhost", UserName = ConnectionFactory.DefaultUser, Password = ConnectionFactory.DefaultPass, Port = AmqpTcpEndpoint.UseDefaultPort };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var message = Serializer.Serialize(pacient);

                        channel.ExchangeDeclare(exchange: "NewPacient", type: ExchangeType.Fanout);
                        channel.BasicPublish(exchange: "NewPacient", routingKey: "", basicProperties: null, body: message);
                    }
                }
            }
            
        }

        public void DeletePacient(string pacient, string email)
        {
            PacientModel deletePacient = new PacientModel()
            {
                userId = pacient,
                emailAddress = email
            };
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = ConnectionFactory.DefaultUser, Password = ConnectionFactory.DefaultPass, Port = AmqpTcpEndpoint.UseDefaultPort };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var message = Serializer.Serialize(deletePacient);

                    channel.ExchangeDeclare(exchange: "DeletePacient", type: ExchangeType.Fanout);
                    channel.BasicPublish(exchange: "DeletePacient", routingKey: "", basicProperties: null, body: message);
                }
            }
        }

        public void DeleteDoctor(string doctor, string email)
        {
            DoctorModel deleteDoctor = new DoctorModel()
            {
                userId = doctor,
                emailAddress = email
            };
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = ConnectionFactory.DefaultUser, Password = ConnectionFactory.DefaultPass, Port = AmqpTcpEndpoint.UseDefaultPort };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var message = Serializer.Serialize(deleteDoctor);

                    channel.ExchangeDeclare(exchange: "DeleteDoctor", type: ExchangeType.Fanout);
                    channel.BasicPublish(exchange: "DeleteDoctor", routingKey: "", basicProperties: null, body: message);
                }
            }
        }
    }
}
