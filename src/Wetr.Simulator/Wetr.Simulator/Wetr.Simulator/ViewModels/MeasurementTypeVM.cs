using Wetr.Simulator.ViewModels;
using Wetr.Server.DAL.DTO;

namespace Wetr.Simulator.ViewModels
{
    public class MeasurementTypeVM : ViewModelBase
    {
        private MeasurementType measurementType;
        public MeasurementType MeasurementType { get { return this.measurementType; } }
        public MeasurementTypeVM(MeasurementType measurementType)
        {
            this.measurementType = measurementType;
        }

        public int ID
        {
            get
            {
                return this.measurementType.ID;
            }
            set
            {
                if (this.measurementType.ID != value)
                {
                    this.measurementType.ID = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public string Description
        {
            get
            {
                return this.measurementType.Description;
            }
            set
            {
                if (this.measurementType.Description != value)
                {
                    this.measurementType.Description = value;
                    this.RaisePropertyChanged();
                }
            }
        }
    }
}