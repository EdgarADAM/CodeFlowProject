using AppointmentService.Transactions;
using AppointmentService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        [HttpGet(Name = "GetAvailability")]
        public List<CalendarModel> GetAvailabilityAppointment(string drID, string Date)
        {
            List<CalendarModel> calendars = new List<CalendarModel>();
            AppointmentsBussines GetDates = new AppointmentsBussines();
            calendars = GetDates.AvailableDates(drID, Date);
            return calendars;
        }
        [HttpPost(Name = "AppointmentEntry")]
        public void AppointmentEntry(string drID, string idPacient, string Date, int idSchedule)
        {
            AppointmentsBussines appointmentEntries = new AppointmentsBussines();
            appointmentEntries.Registry(drID, idPacient, Date, idSchedule);
        }
    }
}
