using UserApi.Models;
using UserApi.Transactions;
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
            Registries insertUser = new Registries();
            insertUser.InsertDBUser(userPost);
        }
        [HttpDelete(Name = "UserDelete")]

        public void DeleteUser(string userId, string type, string email)
        {
            Registries deleteUser = new Registries();
            deleteUser.DeleteUser (userId, type, email);
        }
    }
    
}
