using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Common.Infra.Data.Dapper
{
    public class ConnectionScope : IDisposable
    {        
        private IDbConnection connection;

        public IDbConnection Connection
        {
            get
            {
                if (connection == null || connection.State != ConnectionState.Open)
                {
                    connection = new MySqlConnection(Env.GetString("DB_CONNECTION_STRING"));
                    connection.Open();
                }

                return connection;
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }

                connection.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
