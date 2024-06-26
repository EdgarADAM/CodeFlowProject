using Common.Helpers;

namespace AppointmentService.Transactions
{
    public class AppointmentEntries
    {
        public void Registry(string drID, string idPacient, string date, int idSchedule)
        {
            string query = $@"INSERT INTO Appointment.Appointments VALUES('{drID}', '{idPacient}', '{date}', {idSchedule})";

            SQLQueries sQLQueries = new SQLQueries();
            sQLQueries.ExecScript(query);
        }
    }
}
