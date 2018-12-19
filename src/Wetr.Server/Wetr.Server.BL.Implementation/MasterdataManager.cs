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
        private IConnectionFactory connFac;
        private IMeasurementDeviceDAO measurementDeviceDAO;
        private ICommunityDAO communityDAO;

        public MasterdataManager()
        {
            connFac = new ConnectionFactory("System.Data.SqlClient", "Integrated Security=true;Initial Catalog=WeatherTracer;server=tcp:(local);");
            measurementDeviceDAO = new MeasurementDeviceDAO(connFac);
            communityDAO = new CommunityDAO(connFac);
        }

        public async Task<int> DeleteMeasurementDeviceAsync(MeasurementDevice measurementDevice)
        {
            return await measurementDeviceDAO.DeleteAsync(measurementDevice);
        }
        public async Task<IEnumerable<MeasurementDevice>> FindAllMeasurementDevicesAsync()
        {
            IMeasurementDeviceDAO measurementDeviceDAO = new MeasurementDeviceDAO(connFac);
            return await measurementDeviceDAO.FindAllAsync();
        }
        public async Task<MeasurementDevice> InsertMeasurementDeviceAsync(MeasurementDevice measurementDevice)
        {
            await measurementDeviceDAO.InsertAsync(measurementDevice);
            var id = await measurementDeviceDAO.GetLastIndex();
            measurementDevice.ID = id;
            return measurementDevice;
        }
        public async Task<int> UpdateMeasurementDeviceAsync(MeasurementDevice measurementDevice)
        {
            return await measurementDeviceDAO.UpdateAsync(measurementDevice);
        }

        public async Task<Community> FindCommunityByIdAsync(int id)
        {
            return await communityDAO.FindByIDAsync(id);
        }
    }
}
