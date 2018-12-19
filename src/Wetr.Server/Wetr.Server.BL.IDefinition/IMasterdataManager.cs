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
        Task<IEnumerable<MeasurementDevice>> FindAllMeasurementDevicesAsync();
        Task<int> DeleteMeasurementDeviceAsync(MeasurementDevice measurementDevice);
        Task<int> UpdateMeasurementDeviceAsync(MeasurementDevice measurementDevice);
        Task<MeasurementDevice> InsertMeasurementDeviceAsync(MeasurementDevice measurementDevice);

        Task<Community> FindCommunityByIdAsync(int id);
    }
}
