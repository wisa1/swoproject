using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Server.BL.IDefinition;
using Wetr.Server.Common;
using Wetr.Server.DAL.DAO;
using Wetr.Server.DAL.DTO;
using Wetr.Server.DAL.IDAO;

namespace Wetr.Server.BL.Implementation
{
    public class MasterdataManager : IMasterdataManager
    {
        public async Task<IEnumerable<MeasurementDevice>> FindAllMeasurementDevicesAsync()
        {
            IConnectionFactory connFac = new ConnectionFactory("System.Data.SqlClient", "Integrated Security=true;Initial Catalog=WeatherTracer;server=tcp:(local);");
            
            IMeasurementDeviceDAO measurementDeviceDAO = new MeasurementDeviceDAO(connFac);
            return await measurementDeviceDAO.FindAllAsync();
        }
    }
}
