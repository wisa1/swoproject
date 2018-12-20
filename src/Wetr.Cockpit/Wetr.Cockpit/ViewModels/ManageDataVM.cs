using Wetr.Cockpit.Helpers;
using System.Collections.ObjectModel;
using Wetr.Server.BL.IDefinition;
using System;
using Wetr.Server.DAL.DTO;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;

namespace Wetr.Cockpit.ViewModels
{
    public class ManageDataVM : ViewModelBase
    {
        private ObservableCollection<MeasurementDeviceVM> devices;
        private MeasurementDeviceVM current;
        private ObservableCollection<CommunityVM> communities;

        public ICommand SaveCommand { set; get; }
        public ICommand DeleteCommand { set; get; }
        public ICommand InsertCommand { set; get; }

        public ObservableCollection<MeasurementDeviceVM> Devices {
            get { return devices; }
            set
            {
                if (!this.devices.Equals(value))
                {
                    this.devices = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public ObservableCollection<CommunityVM> Communities
        {
            get{ return communities; }
            set
            {
                if (!this.communities.Equals(value))
                {
                    this.communities = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public MeasurementDeviceVM Current
        {
            get { return current; }
            set
            {
                if (this.current == null || !this.current.Equals(value))
                {
                    this.current = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private IMasterdataManager masterDataManager;
        public ManageDataVM(IMasterdataManager masterDataManager)
        {
            this.masterDataManager = masterDataManager ?? throw new ArgumentException();

            this.communities = new ObservableCollection<CommunityVM>();
            this.devices = new ObservableCollection<MeasurementDeviceVM>();
            LoadData();
            
            this.DeleteCommand = new RelayCommand(DeleteDevice);
            this.SaveCommand = new RelayCommand(UpdateDevice);
            this.InsertCommand = new RelayCommand(InsertDevice);
        }

        private async void InsertDevice(object obj)
        {
            MeasurementDevice measurementDevice = new MeasurementDevice()
            {
                Address = "",
                DeviceName = "New Device",
                Latitude = 0,
                Longitude = 0,
                CommunityID = 1
            };
            measurementDevice = await masterDataManager.InsertMeasurementDeviceAsync(measurementDevice);
            this.devices.Add(new MeasurementDeviceVM(measurementDevice, masterDataManager, this));
            Current = this.devices.Last();
        }
        private async void DeleteDevice(object obj)
        {
            var child = Devices.FirstOrDefault<MeasurementDeviceVM>(vm => vm.Equals(obj));
            if(child != null)
            {
               
                int result = await masterDataManager.DeleteMeasurementDeviceAsync(child.MeasurementDevice);
                if(result != 0)
                {
                    Devices.Remove(child);
                    var next = Devices.Where<MeasurementDeviceVM>(o => o.ID > child.ID).FirstOrDefault();
                    if (next == null)
                    {
                        next = Devices.Where<MeasurementDeviceVM>(o => o.ID < child.ID).LastOrDefault();
                    }
                    Current = next;
                }
                else
                {
                    //Do something in case result is 0 -> nothing deleted (usually because there are measurements for the device in the DB)
                }
            }
        }
        private async void UpdateDevice(object obj)
        {
            var child = Devices.First<MeasurementDeviceVM>(vm => vm.Equals(obj));
            await masterDataManager.UpdateMeasurementDeviceAsync(child.MeasurementDevice);
        }

        private async void LoadData()
        {
            communities.Clear();
            foreach (Community community in await masterDataManager.FindAllCommunitiesAsync())
            {
                communities.Add(new CommunityVM(community, masterDataManager, this));
            }

            Devices.Clear();
            foreach (MeasurementDevice device in await masterDataManager.FindAllMeasurementDevicesAsync())
            {
                Devices.Add(new MeasurementDeviceVM(device, masterDataManager, this));    
            }

            this.Current = Devices.FirstOrDefault();
        }
    }
}
