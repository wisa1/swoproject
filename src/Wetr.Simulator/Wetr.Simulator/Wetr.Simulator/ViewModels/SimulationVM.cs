using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Wetr.Simulator.Helpers;

namespace Wetr.Simulator.ViewModels
{
    public class SimulationVM : ViewModelBase
    {
        #region private fields
        private ObservableCollection<SimulatedMeasurementDeviceVM> simulatedDevices;
        #endregion

        #region public members
        public SimulatorConfiguration Config { set; get; }

        public ObservableCollection<SimulatedMeasurementDeviceVM> SimulatedDevices
        {
            get
            {
                return this.simulatedDevices;
            }
            set
            {
                this.simulatedDevices = value;
                this.RaisePropertyChanged();
            }
        }
        #endregion  

        public SimulationVM(SimulatorConfiguration config, ObservableCollection<MeasurementDeviceVM> devices)
        {
            this.Config = config ?? throw new ArgumentNullException();
            SimulatedDevices = new ObservableCollection<SimulatedMeasurementDeviceVM>();

            //add devices to viewmodel observable
            foreach(var device in devices)
            {
                SimulatedDevices.Add(new SimulatedMeasurementDeviceVM(device.MeasurementDevice));
            }
            //tell simulated devices to start generating values
            foreach(var device in SimulatedDevices)
            {
                device.StartSimulation(config);
            }
        }
    }
}
