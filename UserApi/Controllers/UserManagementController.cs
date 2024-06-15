using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        [HttpPost(Name = "UserRegistry")]
        public void UserRegistry(UserModel userPost)
        {
            Transactions.Transactions insertUser = new Transactions.Transactions();
                insertUser.InsertDBUser(userPost);            
        }
    }
}
