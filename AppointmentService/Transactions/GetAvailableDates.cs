using Common.Helpers;
using Common.Models;
using System.Data;
namespace AppointmentService.Transactions
{
    public class GetAvailableDates
    {
        public DataTable GetDates(string drID, string date)
        {
            DataTable dt = new DataTable();

                string query = $@"SELECT '{drID}' AS idDoctor, '{date}' appointment_day, b.idSchedule, b.description
                            FROM Appointment.schedules b
                            WHERE NOT EXISTS(SELECT 1 FROM Appointment.Appointments c
                            WHERE c.idDoctor = '{drID}'
                            AND c.appointment_day = '{date}'
                            AND b.idSchedule = c.idSchedule )
                            ORDER BY b.idSchedule";

                SQLQueries sQLQueries = new SQLQueries();
                dt = sQLQueries.ExecScript(query);

                return dt;

        }

        public List<CalendarModel> AvailableDates(string drID, string date)
        {
            DataTable dt = new DataTable();
            List<CalendarModel> DatesList = new List<CalendarModel>();

                dt = GetDates(drID, date);
                if (dt.Rows.Count > 0)
                {
                DatesList = (List<CalendarModel>)(from I in dt.AsEnumerable()
                                                        select (new CalendarModel
                                                        {
                                                            drId = I["idDoctor"].ToString() ?? "",
                                                            date = I["appointment_day"].ToString() ?? "",
                                                            idSchedule = I["idSchedule"].ToString() ?? "",
                                                            description = I["description"].ToString() ?? ""
                                                        })).ToList();
                }
           
            return DatesList;
        }
    }
}
