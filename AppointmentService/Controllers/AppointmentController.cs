using Microsoft.AspNetCore.Mvc;

namespace AppointmentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        [HttpGet(Name = "GetAvailability")]
        public void GetAvailabilityAppointment()
        {

        }
    }
}
