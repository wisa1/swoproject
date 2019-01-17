using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Simulator.Helpers;
using Wetr.Simulator.REST;
using Wetr.Simulator.REST.Models;
using Wetr.Simulator.ViewModels;

namespace Wetr.Simulator.DataProvider
{
    class AnonymousCredential : ServiceClientCredentials
    {

    }

    public class WSClient : IRestClient
    {
        public static string baseurl = @"http://localhost:62005";
        RESTClient rc = new RESTClient(new Uri(baseurl), new AnonymousCredential());

        public IEnumerable<MeasurementDevice> GetAllDevices()
        {
            return rc.Devices.GetAllDevices() as IEnumerable<MeasurementDevice>;
        }

        public IEnumerable<MeasurementType> GetAllMeasurementTypes()
        {
            return rc.MeasurementTypes.GetAllTypes();
        }

        public void SaveData(IEnumerable<MeasureModel> data, SimulatorConfiguration config, int deviceId)
        {
            List<Measurement> list = new List<Measurement>();

            foreach (var measurement in data)
            {
                list.Add(new Measurement(null, config.MeasurementType.ID, deviceId, measurement.Value, null, measurement.DateTime));
            }
            rc.Measurements.InsertMultipleMeasurements(list);


        }
    }
}
