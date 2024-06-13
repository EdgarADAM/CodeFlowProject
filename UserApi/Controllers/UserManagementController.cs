using Microsoft.AspNetCore.Mvc;
using UserApi.Helpers;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        [HttpGet(Name = "UserRegistry")]
        public void UserRegistry()
        {
            RabbitReceiver rabbitReceiver = new RabbitReceiver();
            rabbitReceiver.NewUserReceiver();
        }
    }
}
