using Microsoft.AspNetCore.Mvc;
using Common.Helpers;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        [HttpGet(Name = "UserRegistry")]
        public void UserRegistry()
        {
            RabbitMQConnect Receiver = new RabbitMQConnect();
            Receiver.NewUserReceiver();
        }
    }
}
