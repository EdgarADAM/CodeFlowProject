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

            var factory = new ConnectionFactory() { HostName = "localhost" };
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

        public void NewUser(string userID, string type, string email)
        {
            string topic = string.Empty;
            if (type == "1")
            {
                topic = "NewDoctor";
                DoctorModel doctor = new DoctorModel()
                {
                    userId = userID,
                    email = email
                };
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var message = Serializer.Serialize(doctor);

                        channel.ExchangeDeclare(exchange: topic, type: ExchangeType.Fanout);
                        channel.BasicPublish(exchange: topic, routingKey: "", basicProperties: null, body: message);
                    }
                }
                
            }
            else
            {
                topic = "NewPacient";
                PacientModel pacient = new PacientModel()
                {
                    userId = userID,
                    email = email
                };
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var message = Serializer.Serialize(pacient);

                        channel.ExchangeDeclare(exchange: topic, type: ExchangeType.Fanout);
                        channel.BasicPublish(exchange: topic, routingKey: "", basicProperties: null, body: message);
                    }
                }
            }
            
        }

        public void DeletePacient(string pacient, string email)
        {
            PacientModel deletePacient = new PacientModel()
            {
                userId = pacient,
                email = email
            };
            var factory = new ConnectionFactory() { HostName = "localhost" };
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
                email = email
            };
            var factory = new ConnectionFactory() { HostName = "localhost" };
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
