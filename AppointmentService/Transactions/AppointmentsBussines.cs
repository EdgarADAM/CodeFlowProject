using AppointmentService.Models;
using Dapper;
using System.Data.SqlClient;
using AppointmentService.Helpers;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppointmentService.Transactions
{
    public class AppointmentsBussines
    {
        private static string _connectionString = "Server=localhost;Database=Appointments;User Id=Medicaluser;Password=J732yra76W;";

        public List<CalendarModel> AvailableDates(string drID, string date)
        {
            var db = new SqlConnection(_connectionString);

            string query = $@"SELECT @drID AS drId, @date AS date, b.idSchedule, b.description
                            FROM schedules b
                            WHERE NOT EXISTS(SELECT 1 FROM appointments c
                            WHERE c.idDoctor = @drID
                            AND c.appointment_day = @date
                            AND b.idSchedule = c.idSchedule )
                            ORDER BY b.idSchedule";

            var DatesList = db.Query<CalendarModel>(query, new { drID, date });

            return DatesList.ToList();
        }
        public void Registry(string drID, string drName, string idPacient, string pacientName, string pacientEmail, string date, int idSchedule, string descSchedule)
        {
            string query = $@"INSERT INTO appointments VALUES(@drID, @idPacient, @date, @idSchedule)";

            var db = new SqlConnection(_connectionString);

            var id = db.QuerySingle<int>(query, new
            {
                drID,
                idPacient, 
                date, 
                idSchedule
            });
            string subject = "Your appointment was registered successfully";
            DateTime dt = DateTime.ParseExact(date, "yyyyMMdd",
                                  CultureInfo.InvariantCulture);
            string body = $@"Your appontment with DR.{drName} on {dt.ToString("MM-dd-YYYY")} at {descSchedule} was registered successfully!!";
            RabbitSender rabbitSender = new RabbitSender();
            rabbitSender.MessageRabbit(pacientEmail, subject,body);
        }

        public void RegistryDoctorsSchedules(string drID, int idSchedule)
        {
            string query = $@"INSERT INTO drSchedules VALUES(@drID, @idSchedule)";

            var db = new SqlConnection(_connectionString);

            var id = db.QuerySingle<int>(query, new
            {
                drID,
                idSchedule
            });
        }

        public void DeleteDoctorsSchedules(string drId, string scheduleId)
        {
            string query = $@"DELETE drSchedules WHERE drId = @drId AND idSchedule =  @scheduleId";

            var db = new SqlConnection(_connectionString);

            var id = db.QuerySingle<int>(query, new
            {
                drId,
                scheduleId
            });
        }
    }
}
