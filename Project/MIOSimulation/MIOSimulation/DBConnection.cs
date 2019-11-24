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



        public void getArcTimes(Dictionary<String, Int32> memo)
        {

            String sql = "SELECT ARCTIME * FROM ARC";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Int32 from = dr.GetInt32(2);
                Int32 to = dr.GetInt32(3);
                String query = from + " " + to;
                if (!memo.ContainsKey(query))
                {
                    memo.Add(query, dr.GetInt32(1));
                }
            }
        }

    }



}
