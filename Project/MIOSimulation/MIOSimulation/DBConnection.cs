using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace MIOSimulation
{
    class DBConnection
    {

        OracleConnection con;

        public DBConnection()
        {

            initializeConnection();
        }

        public void initializeConnection()
        {
            string oradb = "Data Source=(DESCRIPTION_LIST=" +
                                "(DESCRIPTION =" +
                                "(ADDRESS =" +
                                "(PROTOCOL = TCP)(HOST = 200.3.193.24)(PORT = 1522))" +
                                "(CONNECT_DATA =" +
                                "(SERVICE_NAME = ESTUD)" +
                                ")" +
                                ")" +
                                ");User Id = P09707_1_4; Password = DeVNwu7v;";
            con = new OracleConnection();
            con.ConnectionString = oradb;
            con.Open();
        }

        public void killConnection()
        {
            con.Close();
            con.Dispose();
        }



        public Int32 getArcTime(Int32 fromStop, Int32 toStop)
        {

            String sql = "SELECT ARCTIME FROM ARC WHERE ((STOP_ID_START = :1) AND (STOP_ID_END = :2))";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.Add("1", OracleDbType.Int32).Value = fromStop;
            cmd.Parameters.Add("2", OracleDbType.Int32).Value = toStop;
            OracleDataReader reader = cmd.ExecuteReader();
            Int32 a = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    a = reader.GetInt32(0);
                    break;
                }
            }
;
            return a;
        }

    }



}
