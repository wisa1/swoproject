using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.DAL.DTO;

namespace Wetr.Server.BL.IDefinition
{
    public interface IMasterdataManager
    {
        //Measurement Device
        //--Create
        Task<MeasurementDevice> InsertMeasurementDeviceAsync(MeasurementDevice measurementDevice);

        //--Read
        Task<IEnumerable<MeasurementDevice>> FindAllMeasurementDevicesAsync();
        Task<MeasurementDevice> FindMeasurementDeviceById(int id);

        //--Update
        Task<int> UpdateMeasurementDeviceAsync(MeasurementDevice measurementDevice);

        //--Delete
        Task<int> DeleteMeasurementDeviceAsync(MeasurementDevice measurementDevice);



        Task<Community> FindCommunityByIdAsync(int id);
        Task<IEnumerable<Community>> FindAllCommunitiesAsync();

        Task<IEnumerable<MeasurementType>> FindAllMeasurmentTypesAsync();
    }
}
