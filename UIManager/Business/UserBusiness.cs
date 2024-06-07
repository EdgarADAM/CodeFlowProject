using Common.Helpers;
using Common.Models;

namespace UIManager.Business
{
    internal class UserBusiness
    {
        public void UserRegistry(UserModel newUser)
        {
            RabbitMQConnect registry = new RabbitMQConnect();
            registry.NewUser(newUser);
        }
    }
}
