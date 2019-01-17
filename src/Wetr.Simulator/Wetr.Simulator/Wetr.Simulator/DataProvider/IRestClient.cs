using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Simulator.Helpers;
using Wetr.Simulator.REST.Models;
using Wetr.Simulator.ViewModels;

namespace Wetr.Simulator.DataProvider
{
    public interface IRestClient
    {
        IEnumerable<MeasurementDevice> GetAllDevices();
        IEnumerable<MeasurementType> GetAllMeasurementTypes();
        void SaveData(IEnumerable<MeasureModel> data, SimulatorConfiguration config, int deviceId);
    }
}