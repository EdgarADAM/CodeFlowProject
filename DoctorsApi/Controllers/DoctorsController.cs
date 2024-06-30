using Microsoft.AspNetCore.Mvc;
using DoctorsApi.Models;
using DoctorsApi.Transactions;

namespace DoctorsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        [HttpPatch(Name = "DoctorUpdate")]
         public void DoctorUpdate(DoctorModel doctor)
         {
            Registries updateDoctor = new Registries();
            updateDoctor.UpdateDoctorsProfile(doctor);
         }

        [HttpDelete(Name = "DoctorDelete")]
        public void DoctorDelete(DoctorModel doctor)
        {
            Registries updateDoctor = new Registries();
            updateDoctor.DeleteDoctorsProfile(doctor);
        }

    }
}
