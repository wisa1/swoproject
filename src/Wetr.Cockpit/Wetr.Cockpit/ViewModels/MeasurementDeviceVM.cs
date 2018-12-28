using System.Linq;
using Wetr.Server.BL.IDefinition;
using Wetr.Server.DAL.DTO;

namespace Wetr.Cockpit.ViewModels
{
    public class MeasurementDeviceVM : ViewModelBase
    {
        private MeasurementDevice measurementDevice;
        private IMasterdataManager masterDataManager;
        private ManageDataVM parentVM;
        private CommunityVM community;

        public MeasurementDevice MeasurementDevice { get { return this.measurementDevice; } }
        public CommunityVM Community
        {
            set
            {
                if (value != null)
                {
                    this.CommunityID = value.ID;
                    this.community = value;
                }
            }
            get { return community; }
        }
        public ManageDataVM ParentVM
        {
            get { return this.parentVM; }
        }

        public MeasurementDeviceVM(MeasurementDevice measurementDevice, IMasterdataManager masterDataManager, ManageDataVM parentVM)
        {
            this.measurementDevice = measurementDevice ?? throw new System.ArgumentException("MeasurementDevice must not be null!");
            this.masterDataManager = masterDataManager;
            this.parentVM = parentVM;
            this.Community = parentVM.Communities.Where<CommunityVM>(vm => vm.ID == this.CommunityID).FirstOrDefault();
        }
        public MeasurementDeviceVM(MeasurementDevice measurementDevice, IMasterdataManager masterDataManager, QueryDataVM parentVM)
        {
            this.measurementDevice = measurementDevice ?? throw new System.ArgumentException("MeasurementDevice must not be null!");
            this.masterDataManager = masterDataManager;
        }

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
                    if(this.parentVM != null)
                        this.Community = parentVM.Communities.Where<CommunityVM>(vm => vm.ID == this.CommunityID).FirstOrDefault();
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