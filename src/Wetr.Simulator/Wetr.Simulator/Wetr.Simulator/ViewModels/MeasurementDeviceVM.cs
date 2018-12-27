using System.Linq;
using Wetr.Server.DAL.DTO;
using Wetr.Simulator.ViewModels;

namespace Wetr.Simulator.ViewModels
{
    public class MeasurementDeviceVM : ViewModelBase
    {
        private MeasurementDevice measurementDevice;
        public MeasurementDevice MeasurementDevice { get { return this.measurementDevice; } }

        public MeasurementDeviceVM(MeasurementDevice measurementDevice)
        {
            this.measurementDevice = measurementDevice ?? throw new System.ArgumentException("MeasurementDevice must not be null!");
        }

        public int ID
        {
            get
            {
                return measurementDevice.ID;
            }
        }
        public int CommunityID
        {
            get
            {
                return measurementDevice.CommunityID;
            }
            set
            {
                if (this.measurementDevice.CommunityID != value)
                {
                    this.measurementDevice.CommunityID = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public string DeviceName
        {
            get
            {
                return measurementDevice.DeviceName;
            }
            set
            {
                if (this.measurementDevice.DeviceName != value)
                {
                    this.measurementDevice.DeviceName = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public string Address
        {
            get
            {
                return measurementDevice.Address;
            }
            set
            {
                if (this.measurementDevice.Address != value)
                {
                    this.measurementDevice.Address = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public double Longitude
        {
            get
            {
                return measurementDevice.Longitude;
            }
            set
            {
                if (this.measurementDevice.Longitude != value)
                {
                    this.measurementDevice.Longitude = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public double Latitude
        {
            get
            {
                return measurementDevice.Latitude;
            }
            set
            {
                if (this.measurementDevice.Latitude != value)
                {
                    this.measurementDevice.Latitude = value;
                    this.RaisePropertyChanged();
                }
            }
        }
    }
}