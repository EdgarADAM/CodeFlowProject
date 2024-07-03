using AppointmentService.Transactions;
using AppointmentService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        [HttpGet]
        public List<CalendarModel> GetAvailabilityAppointment(string drID, string Date)
        {
            List<CalendarModel> calendars = new List<CalendarModel>();
            AppointmentsBussines GetDates = new AppointmentsBussines();
            calendars = GetDates.AvailableDates(drID, Date);
            return calendars;
        }
        [HttpPost]
        public void AppointmentEntry(string drID, string drName, string idPacient, string pacientName, string pacientEmail, string date, int idSchedule, string descSchedule)
        {
            AppointmentsBussines appointmentEntries = new AppointmentsBussines();
            appointmentEntries.Registry(drID, drName, idPacient, pacientName, pacientEmail, date, idSchedule, descSchedule);
        }
        [HttpPut]
        public void EntryDoctorsSchedules(string drID, int scheduleId)
        {
            AppointmentsBussines appointmentEntries = new AppointmentsBussines();
            appointmentEntries.RegistryDoctorsSchedules(drID, scheduleId);
        }
        [HttpDelete]
        public void DeleteDoctorsSchedules(string drId, string scheduleId)
        {
            AppointmentsBussines appointmentEntries = new AppointmentsBussines();
            appointmentEntries.DeleteDoctorsSchedules(drId, scheduleId);
        }
    }
}
