using Common.Models;
using Common.Helpers;

namespace UserApi.Transactions
{
    public class Transactions
    {
        SQLQueries queries = new SQLQueries();
        public void InsertDBUser(UserModel user)
        {
            string query = @$"INSERT INTO UserManagement.Pacients VALUES('{user.userId}', '{user.name}')";
            queries.ExecScript(query);
        }
    }
}
