using UserApi.Models;
using Dapper;
using System.Data.SqlClient;
using UserApi.Helpers;

namespace UserApi.Transactions
{
    public class Registries
    {
        private static string _connectionString = "Server=localhost;Database=MedicalAppoint;User Id=Medicaluser;Password=J732yra76W;";

        public void InsertDBUser(UserModel user)
        {
            string query = string.Empty;
            query = @$"INSERT INTO users (type, userId, password, name, emailAddress, birthDate, country, phoneNumber) VALUES(@type, @userId, @password, @name, @emailAddress, @birthDate, @country, @phoneNumber)";

            var db = new SqlConnection(_connectionString);

            db.Query(query, new
            {
                type = user.type,
                userId = user.userId,
                password = user.password,
                name = user.name,
                emailAddress = user.emailAddress,
                birthDate = user.birthDate,
                country = user.country,
                phoneNumber = user.phoneNumber
            });
            string subjectMessage = "Registration completed successfully!!!";
            string bodyMessage = @$"Your user ID is: {user.userId}, please complete the information in your profile";
            RabbitSender messager = new RabbitSender();
            messager.MessageRabbit(user.emailAddress, user.name, subjectMessage, bodyMessage);
            messager.NewUser(user.userId, user.type, user.emailAddress);
        }

        public void DeleteUser(string userId, string type, string email)
        {
            string query = string.Empty;
            query = @$"DELETE users WHERE userId = @userId";

            var db = new SqlConnection(_connectionString);

            db.QuerySingle(query, new
            {
                userId
            });
            if(type == "1")
            {
                RabbitSender messager = new RabbitSender();
                messager.DeleteDoctor(userId, email);
            }
            else
            {
                RabbitSender messager = new RabbitSender();
                messager.DeletePacient(userId, email);
            }
        }
    }
}
