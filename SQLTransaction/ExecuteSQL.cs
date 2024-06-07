using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace IncidentManagement.Helpers
{

    public class ExecuteSQL
    {
        public class ConfigurationSQL
        {
            public string server { get; set; } = null!;
            public string dataBase { get; set; } = null!;
            public string user { get; set; } = null!;
            public string passWord { get; set; } = null!;
            public string timeOut { get; set; } = null!;
            public string integratedSecurity { get; set; } = null!;
            public string trustServerCertificate { get; set; } = null!;
        }
        public class ExecSQL
        {
            private string _connectionString = String.Empty;
            private ConfigurationSQL _configurationSQL;
            public ExecSQL(ConfigurationSQL configurationSQL)
            {
                try
                {
                    _configurationSQL = configurationSQL;

                    CreateConnectionString(configurationSQL);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ES01 - An error occurred when obtaining the data to configure the connection" + ex.Message);
                }
            }
            private void CreateConnectionString(ConfigurationSQL configurationSQL)
            {
                _connectionString = @$"Data Source = {configurationSQL.server}; Initial Catalog ={configurationSQL.dataBase}; User ID = {configurationSQL.user}; Password = {configurationSQL.passWord}; Connection Timeout = {configurationSQL.timeOut}; Integrated Security ={configurationSQL.integratedSecurity}; TrustServerCertificate ={configurationSQL.trustServerCertificate}";
            }
            public string GetConnectionString() 
            { 
             return _connectionString;  
            }
            public DataTable RunScript(string query)
            {
                DataTable dt = new DataTable();
                SqlConnection SqlConn = new SqlConnection(_connectionString);
                SqlCommand cmdCommand = new SqlCommand();
                SqlDataAdapter daExecDs = new SqlDataAdapter();
                try
                {
                    SqlConn.ConnectionString = _connectionString;
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
                catch (Exception ex)
                {
                    
                    Console.WriteLine("Error" + ex.Message);
                }
                finally
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
}
