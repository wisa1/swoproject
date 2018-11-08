using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wer.Server.DAL.DAO;
using Wetr.Server.Common;
using Wetr.Server.DAL.IDAO;

namespace Wetr.Server.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IConnectionFactory connFac = ConnectionFactory.CreateFromConfiguration("WeatherTracer");
            ICommunityDAO comm = new CommunityDAO(connFac);
            
        }
    }
}
