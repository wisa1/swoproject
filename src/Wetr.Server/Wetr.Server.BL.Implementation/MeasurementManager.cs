using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Server.BL.IDefinition;
using Wetr.Server.Common;
using Wetr.Server.DAL.DAO;
using Wetr.Server.DAL.DTO;
using Wetr.Server.DAL.IDAO;

namespace Wetr.Server.BL.Implementation
{
    public class MeasurementManager : IMeasurementManager
    {
        private IConnectionFactory connFac;
        private IMeasurementDAO measurementDAO;
        public MeasurementManager()
        {
            connFac = new ConnectionFactory("System.Data.SqlClient", "Integrated Security=true;Initial Catalog=WeatherTracer;server=tcp:(local);");
            measurementDAO = new MeasurementDAO(connFac);
        }

        public async Task<int> InsertMeasurement(Measurement measurement)
        {
            return await measurementDAO.InsertAsync(measurement);
        }

        public async Task<IEnumerable<GroupedResultRecord>> PerformQueryAsync(MeasurementFilter filter)
        {
            return await measurementDAO.GetAggregatedDataForDevice(filter);
            
        }
    }
}
