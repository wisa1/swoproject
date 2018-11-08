using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wer.Server.DAL.DAO;
using Wetr.Server.Common;
using Wetr.Server.DAL.DTO;
using Wetr.Server.DAL.IDAO;
using Wetr.Server.DTO;

namespace Wetr.Server.TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
           
        }

        private static async Task<int> MainAsync()
        {
            IConnectionFactory connFac = ConnectionFactory.CreateFromConfiguration("WeatherTracer");
            ICommunityDAO comm = new CommunityDAO(connFac);

            ICommunityDAO communityDAO = new CommunityDAO(connFac);
            Community community = await comm.FindByIDAsync(1);


            IUserDAO userDAO = new UserDAO(connFac);
            User user = await userDAO.FindByIdAsync(1);
            Console.WriteLine(user.FirstName);

            return 0;
        }
    }
}
