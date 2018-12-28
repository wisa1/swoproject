using System;
using Wetr.Server.Common;
using static Wetr.Server.Common.Constants;

namespace Wetr.Cockpit.ViewModels
{
    public class GroupedResultRecordVM : ViewModelBase
    {
        private GroupedResultRecord groupedResultRecord;
        private MeasurementTypeVM measurementType;
        private MeasurementDeviceVM measurementDevice;

        public GroupedResultRecord GroupedResultRecord { get { return this.groupedResultRecord; } }

        public GroupedResultRecordVM(GroupedResultRecord groupedResultRecord)
        {
            this.groupedResultRecord = groupedResultRecord;
        }

        public DateTime DateTimeStart
        {
            set
            {
                if (this.groupedResultRecord.DateTimeStart != value)
                {
                    this.groupedResultRecord.DateTimeStart = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.groupedResultRecord.DateTimeStart;
            }
        }
        public PeriodType PeriodType
        {
            set
            {
                if (this.groupedResultRecord.PeriodType != value)
                {
                    this.groupedResultRecord.PeriodType = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.groupedResultRecord.PeriodType;
            }
        }
        public int MeasurementTypeID
        {
            set
            {
                if (this.groupedResultRecord.MeasurementTypeID != value)
                {
                    this.groupedResultRecord.MeasurementTypeID = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.groupedResultRecord.MeasurementTypeID;
            }
        }
        public int UnitOfMeasureID
        {
            set
            {
                if (this.groupedResultRecord.UnitOfMeasureID != value)
                {
                    this.groupedResultRecord.UnitOfMeasureID= value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.groupedResultRecord.UnitOfMeasureID;
            }
        }
        public int DeviceID
        {
            set
            {
                if (this.groupedResultRecord.DeviceID!= value)
                {
                    this.groupedResultRecord.DeviceID= value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.groupedResultRecord.DeviceID;
            }
        }
        public double Value
        {
            set
            {
                if (this.groupedResultRecord.Value != value)
                {
                    this.groupedResultRecord.Value = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.groupedResultRecord.Value;
            }
        }

        public MeasurementTypeVM MeasurementType
        {
            set
            {
                if (this.measurementType == null || this.measurementType != value)
                {
                    this.measurementType = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.measurementType;
            }
        }
        public MeasurementDeviceVM MeasurementDevice
        {
            set
            {
                if (this.measurementDevice == null || this.measurementDevice != value)
                {
                    this.measurementDevice = value;
                    this.RaisePropertyChanged();
                }
            }
            get
            {
                return this.measurementDevice;
            }
        }


    }
}