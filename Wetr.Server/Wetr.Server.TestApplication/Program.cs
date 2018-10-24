using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.Common;

namespace Wetr.Server.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IConnectionFactory connFac = ConnectionFactory.CreateFromConfiguration("WeatherTracer");
            DbConnection conn = connFac.CreateConnection();
            DbCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"select * from province";
            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{0}\t {1}" ,  reader["ID"], reader["Name"] );
            }
        }
    }
}
