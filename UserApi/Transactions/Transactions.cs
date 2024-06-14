using Common.Models;
using Common.Helpers;

namespace UserApi.Transactions
{
    public class Transactions
    {
        SQLQueries queries = new SQLQueries();
        public void InsertDBUser(UserModel user)
        {
            string type = string.Empty;
            if(user.type == "1")
            {
                type = "Doctors";
            }
            else
            {
                type = "Pacients";              
            }
            string query = @$"INSERT INTO UserManagement.{type} VALUES('{user.userId}', '{user.name}')";
            queries.ExecScript(query);
        }
    }
}
