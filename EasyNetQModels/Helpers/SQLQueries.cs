using System.Data;
using System.Data.SqlClient;
using Common.Models;

namespace Common.Helpers
{

    public class SQLQueries
    {
            private string _connInfo = null!;
            public DataTable ExecScript(string query)
            {
                DataTable dt = new DataTable();
                var SqlConn = new SqlConnection();
                SqlCommand cmdCommand = new SqlCommand();
                SqlDataAdapter daExecDs = new SqlDataAdapter();
                try
                {
                    _connInfo = @$"Data Source = localhost; Initial Catalog = MedicalAppoint; User ID = Medicaluser; Password = J732yra76W; Connection Timeout = 50000; Integrated Security = false; TrustServerCertificate = true";
                    SqlConn.ConnectionString = _connInfo;
                    if ((SqlConn.State != ConnectionState.Open))
                    {
                        SqlConn.Open();
                    }
                    cmdCommand.CommandText = query;
                    cmdCommand.CommandType = CommandType.Text;
                    cmdCommand.Connection = SqlConn;

                    daExecDs.SelectCommand = cmdCommand;
                    daExecDs.Fill(dt);
                }
                catch
                {
                    cmdCommand.Dispose();
                    daExecDs.Dispose();
                    SqlConn.Close();
                    SqlConn.Dispose();
                }
                return dt;
        }
    }
}
