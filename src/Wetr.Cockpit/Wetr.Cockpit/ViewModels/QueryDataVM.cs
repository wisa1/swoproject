using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Wetr.Cockpit.Helpers;
using Wetr.Server.BL.IDefinition;
using Wetr.Server.DAL.DTO;
using static Wetr.Server.Common.Constants;

namespace Wetr.Cockpit.ViewModels
{
    public class QueryDataVM : ViewModelBase
    {
        private IMasterdataManager masterDataManager;
        private IMeasurementManager measurementManager;

        private ObservableCollection<MeasurementDeviceVM> devices;
        private ObservableCollection<MeasurementTypeVM> measurementTypes;
        private MeasurementDeviceVM selectedDevice;
        private PeriodType selectedPeriodType;
        private AggregationType selectedAggregationType;
        private MeasurementTypeVM selectedMeasurementType;

        public ObservableCollection<MeasurementDeviceVM> Devices
        {
            set
            {
                if(this.devices != null || !this.devices.Equals(value))
                {
                    this.devices = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.devices;
            }
        }
        public ObservableCollection<MeasurementTypeVM> MeasurementTypes
        {
            set
            {
                if (this.measurementTypes != null || !this.measurementTypes.Equals(value))
                {
                    this.measurementTypes = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.measurementTypes;
            }
        }
        public MeasurementDeviceVM SelectedDevice
        {
            set
            {
                if(selectedDevice == null || !selectedDevice.Equals(value))
                {
                    selectedDevice = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return selectedDevice;
            }
        }
        public PeriodType SelectedPeriodType
        {
            set
            {
                selectedPeriodType = value;
                this.RaisePropertyChanged();
            }
            get
            {
                return selectedPeriodType;
            }
        }
        public AggregationType SelectedAggregationType
        {
            set
            {
                selectedAggregationType = value;
                this.RaisePropertyChanged();
            }
            get
            {
                return selectedAggregationType;
            }
        }
        public MeasurementTypeVM SelectedMeasurementType
        {
            set
            {
                selectedMeasurementType = value;
                this.RaisePropertyChanged();
            }
            get
            {
                return selectedMeasurementType;
            }
        }

        public IEnumerable<PeriodType> PeriodTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(PeriodType)).Cast<PeriodType>();
            }
        }
        public IEnumerable<AggregationType> AggregationTypeValues
        {
            get
            {
                return Enum.GetValues(typeof(AggregationType)).Cast<AggregationType>();
            }
        }

        public ICommand QueryCommand;

        public QueryDataVM(IMasterdataManager masterDataManager, IMeasurementManager measurementManager)
        {
            this.masterDataManager = masterDataManager;
            this.measurementManager = measurementManager;

            this.QueryCommand = new RelayCommand(PerformQuery);

            LoadData();
        }

        private void PerformQuery(object obj)
        {
            throw new NotImplementedException();
        }

        private async void LoadData()
        {
            this.devices?.Clear();
            this.devices = new ObservableCollection<MeasurementDeviceVM>();
            foreach (MeasurementDevice device in await masterDataManager.FindAllMeasurementDevicesAsync())
            {
                this.devices.Add(new MeasurementDeviceVM(device, masterDataManager, this));
            }

            this.measurementTypes?.Clear();
            this.measurementTypes = new ObservableCollection<MeasurementTypeVM>();
            foreach(MeasurementType type in await masterDataManager.FindAllMeasurmentTypesAsync())
            {
                this.measurementTypes.Add(new MeasurementTypeVM(type, masterDataManager));
            }
        }
    }
}
