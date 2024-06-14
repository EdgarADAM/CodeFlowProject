using Common.Models;
using Microsoft.AspNetCore.Mvc;
using UserApi.Helpers;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        [HttpPost(Name = "UserRegistry")]
        public void UserRegistry(UserModel userPost)
        {
            RabbitReceiver rabbitReceiver = new RabbitReceiver();
            if (userPost == null)
            {
                rabbitReceiver.NewUserReceiver();
            }
            else 
            {
                Transactions.Transactions insertUser = new Transactions.Transactions();
                insertUser.InsertDBUser(userPost);
            }
            
        }
    }
}
