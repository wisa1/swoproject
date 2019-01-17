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
        private IMeasurementTypeDAO measurementTypeDAO;

        public MasterdataManager()
        {
            connFac = new ConnectionFactory("System.Data.SqlClient", "Integrated Security=true;Initial Catalog=WeatherTracer;server=tcp:(local);");
            measurementDeviceDAO = new MeasurementDeviceDAO(connFac);
            measurementTypeDAO = new MeasurementTypeDAO(connFac);
            communityDAO = new CommunityDAO(connFac);
        }

        //MeasurementDevice
        //--Create
        public async Task<MeasurementDevice> InsertMeasurementDeviceAsync(MeasurementDevice measurementDevice)
        {
            await measurementDeviceDAO.InsertAsync(measurementDevice);
            var id = await measurementDeviceDAO.GetLastIndex();
            measurementDevice.ID = id;
            return measurementDevice;
        }

        //--Read
        public async Task<IEnumerable<MeasurementDevice>> FindAllMeasurementDevicesAsync()
        {
            return await this.measurementDeviceDAO.FindAllAsync();
        }
        public async Task<MeasurementDevice> FindMeasurementDeviceById(int id)
        {
            return await measurementDeviceDAO.FindByIDAsync(id);
        }

        //--Update
        public async Task<int> UpdateMeasurementDeviceAsync(MeasurementDevice measurementDevice)
        {
            return await measurementDeviceDAO.UpdateAsync(measurementDevice);
        }

        //--Delete
        public async Task<int> DeleteMeasurementDeviceAsync(MeasurementDevice measurementDevice)
        {
            return await measurementDeviceDAO.DeleteAsync(measurementDevice);
        }
        

        //Community
        public async Task<Community> FindCommunityByIdAsync(int id)
        {
            return await communityDAO.FindByIDAsync(id);
        }
        public async Task<IEnumerable<Community>> FindAllCommunitiesAsync()
        {
            return await this.communityDAO.FindAllAsync();
        }

        //MeasurementType
        public async Task<IEnumerable<MeasurementType>> FindAllMeasurmentTypesAsync()
        {
            return await this.measurementTypeDAO.FindAllAsync();
        }
    }
}
