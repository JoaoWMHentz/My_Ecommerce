using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Data.SqlClient;
namespace Helpers.connection
{
    public class Connection : IDisposable
    {
        public SqlConnection sqlConnection = new SqlConnection();
        //Construtor
        public Connection()
        {
            sqlConnection.ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Ecommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        // conectar
        public SqlConnection Connect()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            return sqlConnection;
        }

        // Desconectar

        public void Disconnect()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
