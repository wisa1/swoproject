using Wetr.Cockpit.Helpers;
using System.Collections.ObjectModel;
using Wetr.Server.BL.IDefinition;
using System;
using Wetr.Server.DAL.DTO;
using System.Collections.Generic;

namespace Wetr.Cockpit.ViewModels
{
    public class ManageDataViewModel : ViewModelBase
    {
        private ObservableCollection<MeasurementDeviceVM> devices;
        private MeasurementDeviceVM current;

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
        public MeasurementDeviceVM Current
        {
            get { return current; }
            set
            {
                if (!this.current.Equals(value))
                {
                    this.current = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private IMasterdataManager masterDataManager;

        public ManageDataViewModel(IMasterdataManager masterDataManager)
        {
            this.masterDataManager = masterDataManager ?? throw new ArgumentException();
            this.devices = new ObservableCollection<MeasurementDeviceVM>();
            LoadAllDevices();
        }

        private async void LoadAllDevices()
        {
            Devices.Clear();
            foreach (MeasurementDevice device in await masterDataManager.FindAllMeasurementDevicesAsync())
            {
                Devices.Add(new MeasurementDeviceVM(device));
            }
        }
    }
}
