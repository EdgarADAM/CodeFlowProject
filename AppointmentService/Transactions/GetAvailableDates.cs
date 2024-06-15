using Common.Models;
namespace AppointmentService.Transactions
{
    public class GetAvailableDates
    {
        public CalendarModel AvailableDates(string drID, string day)
        {
            string query = $@"SELEC {day} as day";
            return new CalendarModel();
        }
    }
}
