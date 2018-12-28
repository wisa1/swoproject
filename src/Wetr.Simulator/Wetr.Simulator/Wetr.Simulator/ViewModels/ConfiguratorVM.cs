using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Wetr.Server.BL.IDefinition;
using Wetr.Simulator.DataProvider;
using Wetr.Simulator.Helpers;
using Wetr.Simulator.ViewModels;
using static Wetr.Server.Common.Constants;

namespace Wetr.Simulator.ViewModels
{
    public class ConfiguratorVM : ViewModelBase
    {
        #region private fields
        private IRestClient restClient;        

        private DateTime _startDate;
        private DateTime _endDate;
        private ObservableCollection<MeasurementTypeVM> _measurementTypeVMs;
        private MeasurementTypeVM _selectedMeasurementType;

        private ObservableCollection<MeasurementDeviceVM> _availableMeasurementDeviceVMs;
        private ObservableCollection<MeasurementDeviceVM> _selectedMeasurementDeviceVMs;

        private MeasurementDeviceVM _selectedAvailableDevice;
        private MeasurementDeviceVM _selectedSelectedDevice;

        private float _rangeFrom;
        private float _rangeTo;
        private float _simulationSpeed;
        private DistributionStrategy _selectedDistributionStrategy;
        private int _hoursTimespan;
        #endregion

        #region public properties
        public DateTime StartDate
        {
            set
            {
                if(value != _startDate)
                {
                    this._startDate = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {

                return (this._startDate - DateTime.MinValue).TotalDays == 0 ? DateTime.Now.AddDays(-7) : this._startDate;
            }

        }
        public DateTime EndDate
        {
            set
            {
                if (value != _endDate)
                {
                    this._endDate = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return (this._endDate - DateTime.MinValue).TotalDays == 0 ? DateTime.Now.AddDays(7) : this._endDate;
            }
        }
        public ObservableCollection<MeasurementTypeVM> MeasurementTypeVMs
        {
            set
            {
                if (value != _measurementTypeVMs)
                {
                    this._measurementTypeVMs = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this._measurementTypeVMs;
            }
        }
        public ObservableCollection<MeasurementDeviceVM> AvailableMeasurementDeviceVMs
        {
            set
            {
                if (value != _availableMeasurementDeviceVMs)
                {
                    this._availableMeasurementDeviceVMs = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this._availableMeasurementDeviceVMs;
            }
        }

        public MeasurementDeviceVM SelectedAvailableDevice
        {
            set
            {
                if(this._selectedAvailableDevice != value)
                {
                    this._selectedAvailableDevice = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this._selectedAvailableDevice;
            }
        }
        public MeasurementDeviceVM SelectedSelectedDevice
        {
            set
            {
                if (this._selectedSelectedDevice != value)
                {
                    this._selectedSelectedDevice = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this._selectedSelectedDevice;
            }
        }
        public ObservableCollection<MeasurementDeviceVM> SelectedMeasurementDeviceVMs
        {
            set
            {
                if (value != this._selectedMeasurementDeviceVMs)
                {
                    this._selectedMeasurementDeviceVMs = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this._selectedMeasurementDeviceVMs;
            }
        }

        public float RangeFrom
        {
            set
            {
                if (value != _rangeFrom)
                {
                    this._rangeFrom = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this._rangeFrom;
            }
        }
        public float RangeTo
        {
            set
            {
                if (value != _rangeTo)
                {
                    this._rangeTo = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this._rangeTo;
            }
        }
        public float SimulationSpeed
        {
            set
            {
                if (value != _simulationSpeed)
                {
                    this._simulationSpeed = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                if (this._simulationSpeed < 1)
                    this._simulationSpeed = 1;
                return this._simulationSpeed;
            }
        }
        public DistributionStrategy SelectedDistributionStrategy
        {
            set
            {
                if (value != _selectedDistributionStrategy)
                {
                    this._selectedDistributionStrategy = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this._selectedDistributionStrategy;
            }
        }
        public IEnumerable<DistributionStrategy> DistributionStrategyValues
        {
            get
            {
                return Enum.GetValues(typeof(DistributionStrategy)).Cast<DistributionStrategy>();
            }
        }
        public int HoursTimespan
        {
            set
            {
                if (value != _hoursTimespan)
                {
                    this._hoursTimespan = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this._hoursTimespan;
            }
        }
        public MeasurementTypeVM SelectedMeasurementType
        {
            get
            {
                return this._selectedMeasurementType;
            }
            set
            {
                if(this._selectedMeasurementType != value)
                {
                    this._selectedMeasurementType = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public ICommand AddDeviceCommand { set; get; }
        public ICommand RemoveDeviceCommand { set; get; }
        public ICommand StartSimulation { set; get; }
        #endregion

        public ConfiguratorVM(IRestClient restClient)
        {
            this.restClient = restClient;

            this.AddDeviceCommand = new RelayCommand(AddDevice);
            this.RemoveDeviceCommand = new RelayCommand(RemoveDevice);

            this.AvailableMeasurementDeviceVMs = new ObservableCollection<MeasurementDeviceVM>();
            foreach(var device in restClient.GetAllDevices())
            {
                this.AvailableMeasurementDeviceVMs.Add(new SimulatedMeasurementDeviceVM(device));
                this.RaisePropertyChanged("AvailableMeasurementDeviceVMs");
            }

            this.SelectedMeasurementDeviceVMs = new ObservableCollection<MeasurementDeviceVM>();

            this.MeasurementTypeVMs = new ObservableCollection<MeasurementTypeVM>();
            foreach (var type in restClient.GetAllMeasurementTypes())
            {
                this.MeasurementTypeVMs.Add(new MeasurementTypeVM(type));
                this.RaisePropertyChanged("MeasurementTypeVMs");
            }

            this.SelectedMeasurementType = this.MeasurementTypeVMs.FirstOrDefault();
        }
        private void RemoveDevice(object obj)
        {
            if (this.SelectedSelectedDevice != null)
            {
                this.AvailableMeasurementDeviceVMs.Add(this.SelectedSelectedDevice);
                this.RaisePropertyChanged("AvailableMeasurementDeviceVMs");

                this.SelectedMeasurementDeviceVMs.Remove(this.SelectedSelectedDevice);
                this.RaisePropertyChanged("SelectedMeasurementDeviceVMs");
            }
        }
        private void AddDevice(object obj)
        {
            if(this.SelectedAvailableDevice != null)
            {
                this.SelectedMeasurementDeviceVMs.Add(this.SelectedAvailableDevice);
                this.RaisePropertyChanged("SelectedMeasurementDeviceVMs");

                this.AvailableMeasurementDeviceVMs.Remove(this.SelectedAvailableDevice);
                this.RaisePropertyChanged("AvailableMeasurementDeviceVMs");
            }
        }
        public SimulatorConfiguration BundleConfiguration()
        {
            return new SimulatorConfiguration(this.StartDate,
                this.EndDate,
                this.SelectedMeasurementType,
                this.RangeFrom,
                this.RangeTo,
                this.SimulationSpeed,
                this.SelectedDistributionStrategy,
                this.HoursTimespan
            );       
        }
    }
}
