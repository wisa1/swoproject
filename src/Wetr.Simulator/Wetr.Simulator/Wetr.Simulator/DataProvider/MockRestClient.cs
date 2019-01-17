using System;
using System.Collections.Generic;
using Wetr.Simulator.Helpers;
using Wetr.Simulator.REST.Models;
using Wetr.Simulator.ViewModels;

namespace Wetr.Simulator.DataProvider
{
    class MockRestClient : IRestClient
    {
        private List<MeasurementDevice> _measurementDevices;
        private List<MeasurementType> _measurementTypes;

        public MockRestClient()
        {
            LoadData();
        }

        private void LoadData()
        {
            //Mock-Data
            _measurementDevices = new List<MeasurementDevice>()
            {
                new MeasurementDevice()
                {
                    ID = 1,
                    Address = "Softwarepark 49",
                    CommunityID = 3,
                    DeviceName = "Test-Device 1",
                    Latitude = 11.123456,
                    Longitude = 36.123345
                },
                new MeasurementDevice()
                {
                    ID = 2,
                    Address = "Haupstraße 3",
                    CommunityID = 6,
                    DeviceName = "Test-Device 2",
                    Latitude = 12.123456,
                    Longitude = 37.123345
                },
                new MeasurementDevice()
                {
                    ID = 3,
                    Address = "Gasse 42",
                    CommunityID = 8,
                    DeviceName = "Test-Device 3",
                    Latitude = 13.123456,
                    Longitude = 38.123345
                },
                new MeasurementDevice()
                {
                    ID = 4,
                    Address = "Straße 69",
                    CommunityID = 2,
                    DeviceName = "Test-Device 4",
                    Latitude = 14.123456,
                    Longitude = 39.123345
                },
                new MeasurementDevice()
                {
                    ID = 5,
                    Address = "Hubertweg 11",
                    CommunityID = 1,
                    DeviceName = "Test-Device 5",
                    Latitude = 16.123456,
                    Longitude = 41.123345
                }
            };
            _measurementTypes = new List<MeasurementType>()
            {
                new MeasurementType()
                {
                    ID = 1,
                    Description = "Air-Temperature"
                },
                new MeasurementType()
                {
                    ID = 2,
                    Description = "Air-Pressure"
                },
                new MeasurementType()
                {
                    ID = 3,
                    Description = "Air-Humidity"
                },
                new MeasurementType()
                {
                    ID = 4,
                    Description = "Rain-volume"
                },
                new MeasurementType()
                {
                    ID = 5,
                    Description = "Wind-speed"
                },
                new MeasurementType()
                {
                    ID = 6,
                    Description = "Wind-Direction"
                }
            };
        }

        public IEnumerable<MeasurementDevice> GetAllDevices()
        {
            return this._measurementDevices;
        }

        public IEnumerable<MeasurementType> GetAllMeasurementTypes()
        {
            return this._measurementTypes;
        }

        public void SaveData(IEnumerable<MeasureModel> data, SimulatorConfiguration config, int deviceId)
        {
            //TODO once the rest service stands...
        }
    }
}
