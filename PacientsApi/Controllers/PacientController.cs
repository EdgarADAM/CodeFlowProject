using Microsoft.AspNetCore.Mvc;
using Pacients.Transactions;
using PacientsApi.Models;

namespace PacientsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacientController : ControllerBase
    {
        [HttpPatch]
        public void PacientUpdate(PacientModel pacient)
        {
            Registries updatePacient = new Registries();
            updatePacient.UpdatePacientsProfile(pacient);
        }

        [HttpDelete]
        public void PacientDelete(PacientModel pacient)
        {
            Registries updatePacient = new Registries();
            updatePacient.DeletePacientsProfile(pacient);
        }

    }
}
