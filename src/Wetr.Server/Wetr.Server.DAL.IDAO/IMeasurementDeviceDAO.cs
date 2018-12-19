using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.DAL.DTO;

namespace Wetr.Server.DAL.IDAO
{
    public interface IMeasurementDeviceDAO
    {
        Task<IEnumerable<MeasurementDevice>> FindAllAsync();
        Task<MeasurementDevice> FindByIDAsync(int id);
        Task<int> InsertAsync(MeasurementDevice measurementDevice);
        Task<int> DeleteAsync(MeasurementDevice measurementDevice);
        Task<int> UpdateAsync(MeasurementDevice measurementDevice);
        Task<int> GetLastIndex();
    }
}
