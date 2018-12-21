using Wetr.Server.BL.IDefinition;
using Wetr.Server.DAL.DTO;

namespace Wetr.Cockpit.ViewModels
{
    public class MeasurementTypeVM : ViewModelBase
    {
        private MeasurementType measurementType;
        private IMasterdataManager masterDataManager;

        public MeasurementType MeasurementType { get { return this.measurementType; } }
        public MeasurementTypeVM(MeasurementType measurementType, IMasterdataManager masterDataManager)
        {
            this.measurementType = measurementType;
            this.masterDataManager = masterDataManager;
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