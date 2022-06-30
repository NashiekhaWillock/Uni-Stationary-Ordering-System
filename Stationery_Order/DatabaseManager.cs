using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace Stationery_Order
{
    public class DatabaseManager
    {
        private readonly string DB_CONNECTION_STRING;

        public DatabaseManager(string username, string pwd)
        {
            DB_CONNECTION_STRING = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = crusstuora1.staffs.ac.uk)(PORT = 1521))"
                    + "(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = stora)));User Id=" + username + ";Password=" + pwd + ";";
        }

        public void CloseConnection(OracleConnection conn)
        {
            if (conn != null)
            {
                try
                {
                    conn.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("ERROR: closure of database connection failed", e);
                }
            }
        }

        public OracleConnection GetConnection()
        {
            OracleConnection conn = null;

            try
            {
                conn = new OracleConnection(DB_CONNECTION_STRING);
                conn.Open();
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: connection to database failed", e);
            }

            return conn;
        }

        public void InitialiseDatabase()
        {
            OracleConnection conn = GetConnection();

            OracleCommand StockTable = new OracleCommand
            {
                Connection = conn,
                CommandText = "Select * From Stock",
                CommandType = CommandType.Text
            };
            try
            {
                StockTable.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: SQL command failed", e);
            }

            CloseConnection(conn);
        }
    }
}
