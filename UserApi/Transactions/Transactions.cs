using UserApi.Models;
using Dapper;
using System.Data.SqlClient;
using UserApi.Helpers;

namespace UserApi.Transactions
{
    public class Registries
    {
        private static string _connectionString = "Server=localhost;Database=UsersMedicalAppoint;User Id=Medicaluser;Password=J732yra76W;";

        public void InsertDBUser(UserModel user)
        {
            string query = string.Empty;
            query = @$"INSERT INTO users (type, userId, password) VALUES(@type, @userId, @password)";

            var db = new SqlConnection(_connectionString);

            db.Query(query, new
            {
                type = user.type,
                userId = user.userId,
                password = user.password
            });
            string subjectMessage = "Registration completed successfully!!!";
            string bodyMessage = @$"Your user ID is: {user.userId}, please complete the information in your profile";
            RabbitSender messager = new RabbitSender();
            messager.MessageRabbit(user.emailAddress, user.name, subjectMessage, bodyMessage);
            messager.NewUser(user);
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
