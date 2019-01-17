using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Wetr.Cockpit.Helpers;
using Wetr.Server.BL.IDefinition;
using Wetr.Server.Common;
using Wetr.Server.DAL.DTO;
using static Wetr.Server.Common.Constants;

namespace Wetr.Cockpit.ViewModels
{
    public class QueryDataVM : ViewModelBase, IDataErrorInfo
    {
        private IMasterdataManager masterDataManager;
        private IMeasurementManager measurementManager;

        private ObservableCollection<MeasurementDeviceVM> devices;
        private ObservableCollection<MeasurementTypeVM> measurementTypes;
        private ObservableCollection<GroupedResultRecordVM> queryResultRecords;

        private MeasurementDeviceVM selectedDevice;
        private PeriodType selectedPeriodType;
        private AggregationType selectedAggregationType;
        private MeasurementTypeVM selectedMeasurementType;

        private DateTime dateTimeFrom;
        private DateTime dateTimeTo;
        private int radiusKm;

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
        public ObservableCollection<GroupedResultRecordVM> QueryResultRecords
        {
            set
            {
                if (this.queryResultRecords != null || !this.queryResultRecords.Equals(value))
                {
                    this.queryResultRecords = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.queryResultRecords;
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
                this.RaisePropertyChanged("SelectedAggregationType");
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
                this.RaisePropertyChanged("SelectedPeriodType");
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
        public DateTime DateTimeFrom
        {
            set
            {
                if(this.dateTimeFrom == null || this.dateTimeFrom != value)
                {
                    this.dateTimeFrom = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.dateTimeFrom;  
            }
        }
        public DateTime DateTimeTo
        {
            set
            {
                if (this.dateTimeTo == null || this.dateTimeTo != value)
                {
                    this.dateTimeTo = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.dateTimeTo;
            }
        }
        public int RadiusKm
        {
            set
            {
                if (this.radiusKm != value)
                {
                    this.radiusKm = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.radiusKm;
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
        public ICommand QueryCommand{ get; set; }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                if(columnName == "SelectedPeriodType")
                {
                    if(this.SelectedAggregationType == AggregationType.None && this.SelectedPeriodType != PeriodType.None)
                    {
                        return "Invalid Aggregation/Period Settings";
                    }
                }

                if (columnName == "SelectedAggregationType")
                {
                    if (this.SelectedAggregationType == AggregationType.None && this.SelectedPeriodType != PeriodType.None)
                    {
                        return "Invalid Aggregation/Period Settings";
                    }
                }

                if (columnName == "RadiusKm")
                {
                    return string.Empty;
                }
                    
                return string.Empty;
            }
        }


        public QueryDataVM(IMasterdataManager masterDataManager, IMeasurementManager measurementManager)
        {
            
            this.masterDataManager = masterDataManager;
            this.measurementManager = measurementManager;

            LoadData();

            this.QueryCommand = new RelayCommand(PerformQuery);
            this.dateTimeFrom = DateTime.Today.AddDays(-7);
            this.dateTimeTo = DateTime.Today;

            
        }
        private async void PerformQuery(object obj)
        {
            MeasurementFilter mf = new MeasurementFilter()
            {
                AggregationType = this.SelectedAggregationType,
                PeriodType = this.SelectedPeriodType,
                MeasurementDevice = this.SelectedDevice?.MeasurementDevice,
                MeasurementType = this.selectedMeasurementType?.MeasurementType,
                DateFrom = this.DateTimeFrom,
                DateTo = this.DateTimeTo,
                RadiusKm = 10
                
            };

            if(mf.PeriodType != PeriodType.None && mf.AggregationType == AggregationType.None)
            {
                return;
            }

            this.QueryResultRecords?.Clear();
            this.queryResultRecords = new ObservableCollection<GroupedResultRecordVM>();
            foreach (GroupedResultRecord record in await this.measurementManager.PerformQueryAsync(mf))
            {
                var res = new GroupedResultRecordVM(record);
                res.MeasurementDevice = Devices.Where<MeasurementDeviceVM>(vm => vm.ID == res.DeviceID)?.FirstOrDefault();

                res.MeasurementType = MeasurementTypes.Where<MeasurementTypeVM>(vm => vm.ID == res.MeasurementTypeID)?.FirstOrDefault();
                QueryResultRecords.Add(res);
            }
            this.RaisePropertyChanged("QueryResultRecords");
            
        }
        private async void LoadData()
        {
            this.devices?.Clear();
            this.devices = new ObservableCollection<MeasurementDeviceVM>();
            foreach (MeasurementDevice device in await masterDataManager.FindAllMeasurementDevicesAsync())
            {
                this.devices.Add(new MeasurementDeviceVM(device, masterDataManager, this));
            }
            this.RaisePropertyChanged("Devices");

            this.measurementTypes?.Clear();
            this.measurementTypes = new ObservableCollection<MeasurementTypeVM>();
            foreach(MeasurementType type in await masterDataManager.FindAllMeasurmentTypesAsync())
            {
                this.measurementTypes.Add(new MeasurementTypeVM(type /*, masterDataManager*/));
            }
            this.RaisePropertyChanged("MeasurementTypes");
        }
    }
}
