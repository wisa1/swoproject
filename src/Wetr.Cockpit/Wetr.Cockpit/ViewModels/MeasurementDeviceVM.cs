
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Wetr.Cockpit.Helpers;
using Wetr.Server.BL.IDefinition;
using Wetr.Server.DAL.DTO;

namespace Wetr.Cockpit.ViewModels
{
    public class MeasurementDeviceVM : ViewModelBase
    {
        private MeasurementDevice measurementDevice;
        IMasterdataManager masterDataManager;

        public Community Community;
        public MeasurementDeviceVM(MeasurementDevice measurementDevice, IMasterdataManager masterDataManager)
        {
            this.measurementDevice = measurementDevice ?? throw new System.ArgumentException("MeasurementDevice must not be null!");
            this.masterDataManager = masterDataManager;
        }

        public MeasurementDevice MeasurementDevice{ get { return this.measurementDevice; }}

        public int ID
        {
            get
            {
                return measurementDevice.ID;
            }
        }
        public int CommunityID {
            get
            {
                return measurementDevice.CommunityID;
            }
            set
            {
                if (this.measurementDevice.CommunityID != value)
                {
                    this.measurementDevice.CommunityID = value;
                    //this.Community = masterDataManager.FindCommunityByIdAsync(value).Result;
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