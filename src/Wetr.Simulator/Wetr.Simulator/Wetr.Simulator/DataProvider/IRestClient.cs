using System.Collections.Generic;
using Wetr.Server.DAL.DTO;

namespace Wetr.Simulator.DataProvider
{
    public interface IRestClient
    {
        IEnumerable<MeasurementDevice> GetAllDevices();
        IEnumerable<MeasurementType> GetAllMeasurementTypes();
    }
}