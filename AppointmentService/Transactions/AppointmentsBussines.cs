using AppointmentService.Models;
using Dapper;
using System.Data.SqlClient;
using AppointmentService.Helpers;

namespace AppointmentService.Transactions
{
    public class AppointmentsBussines
    {
        private static string _connectionString = "Server=localhost;Database=MedicalAppoint;User Id=Medicaluser;Password=J732yra76W;";

        public List<CalendarModel> AvailableDates(string drID, string date)
        {
            var db = new SqlConnection(_connectionString);

            string query = $@"SELECT @drID AS drId, @date AS date, b.idSchedule, b.description
                            FROM Appointment.schedules b
                            WHERE NOT EXISTS(SELECT 1 FROM Appointment.Appointments c
                            WHERE c.idDoctor = @drID
                            AND c.appointment_day = @date
                            AND b.idSchedule = c.idSchedule )
                            ORDER BY b.idSchedule";

            var DatesList = db.Query<CalendarModel>(query, new { drID, date });

            return DatesList.ToList();
        }
        public void Registry(string drID, string idPacient, string date, int idSchedule)
        {
            string query = $@"INSERT INTO Appointment.Appointments VALUES(@drID, @idPacient, @date, @idSchedule)";

            var db = new SqlConnection(_connectionString);

            var id = db.QuerySingle<int>(query, new
            {
                drID,
                idPacient, 
                date, 
                idSchedule
            });

            RabbitSender rabbitSender = new RabbitSender();
            rabbitSender.MessageRabbit();
        }
    }
}
