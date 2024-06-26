using AppointmentService.Transactions;
using Common.Models;
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
            GetAvailableDates GetDates = new GetAvailableDates();
            calendars = GetDates.AvailableDates(drID, Date);
            return calendars;
        }
        [HttpPost(Name = "AppointmentEntry")]
        public void AppointmentEntry(string drID, string idPacient, string Date, int idSchedule)
        {
            AppointmentEntries appointmentEntries = new AppointmentEntries();
            appointmentEntries.Registry(drID, idPacient, Date, idSchedule);
        }
    }
}
