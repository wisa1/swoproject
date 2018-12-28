using System.Collections.Generic;
using Wetr.Server.DAL.DTO;
using Wetr.Simulator.Helpers;
using Wetr.Simulator.ViewModels;

namespace Wetr.Simulator.DataProvider
{
    public interface IRestClient
    {
        IEnumerable<MeasurementDevice> GetAllDevices();
        IEnumerable<MeasurementType> GetAllMeasurementTypes();
        void SaveData(IEnumerable<MeasureModel> data, SimulatorConfiguration config);
    }
}