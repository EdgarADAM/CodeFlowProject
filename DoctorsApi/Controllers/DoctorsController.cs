using Microsoft.AspNetCore.Mvc;
using DoctorsApi.Models;
using DoctorsApi.Transactions;

namespace DoctorsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        [HttpGet]
        public List<DoctorModel> GetAllDoctors()
        {
            Registries updateDoctor = new Registries();
            return updateDoctor.GetDoctorsProfile();
        }

        [HttpPatch]
         public void DoctorUpdate(DoctorModel doctor)
         {
            Registries updateDoctor = new Registries();
            updateDoctor.UpdateDoctorsProfile(doctor);
         }

        [HttpDelete]
        public void DoctorDelete(DoctorModel doctor)
        {
            Registries updateDoctor = new Registries();
            updateDoctor.DeleteDoctorsProfile(doctor);
        }

    }
}
