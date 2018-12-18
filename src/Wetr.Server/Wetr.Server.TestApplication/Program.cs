using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.DAL.DAO;
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
            var users = await userDAO.FindAllAsync();
            User user = await userDAO.FindByIdAsync(1);

            Console.WriteLine(user.FirstName);

            IMeasurementDeviceDAO measurementDeviceDAO = new MeasurementDeviceDAO(connFac);
            var md = await measurementDeviceDAO.FindByIDAsync(1);
            Console.WriteLine(md.Address);

            md.Address = "Röhrgraben 7";
            await measurementDeviceDAO.UpdateAsync(md);

            var md2 = await measurementDeviceDAO.FindByIDAsync(1);
            Console.WriteLine(md2.Address);

            int x = await measurementDeviceDAO.DeleteAsync(md2);
            Console.WriteLine(x);

            var md3 = await measurementDeviceDAO.FindByIDAsync(1);
            Console.WriteLine(md.Address);

            Console.ReadLine();

            var measurementDao = new MeasurementDAO(connFac);
            var measurementTypeDao = new MeasurementTypeDAO(connFac);

            var device = await measurementDeviceDAO.FindByIDAsync(1);
            var type = await measurementTypeDao.FindByIDAsync(1);

            var recs = await measurementDao.GetAggregatedDataForDevice(device, Constants.AggregationType.Max, type, Constants.PeriodType.Month);
            return 0;
        }
    }
}
