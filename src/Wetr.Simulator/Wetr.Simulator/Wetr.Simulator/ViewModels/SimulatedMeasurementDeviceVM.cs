using LiveCharts;
using System;
using System.Windows.Input;
using System.Windows.Threading;
using Wetr.Simulator.DataProvider;
using Wetr.Simulator.Helpers;
using Wetr.Simulator.REST.Models;
using Wetr.Simulator.SimulatorLogic;

namespace Wetr.Simulator.ViewModels
{
    public class SimulatedMeasurementDeviceVM : MeasurementDeviceVM
    {
        

        private DispatcherTimer timer;

        private SimulatorConfiguration config;
        private ChartValues<MeasureModel> chartValues;
        private double _axisMax;
        private double _axisMin;

        public ICommand StopSimulationCommand { set; get; }
        public ICommand SaveSimulatedDataCommand { set; get; }

        private DateTime lastValueDate;
        private double lastValue;
        public Func<double, string> Formatter { set; get; }
        public bool valuesSaved = false;

        public SimulatedMeasurementDeviceVM(MeasurementDevice device) : base(device)
        {
            this.chartValues = new ChartValues<MeasureModel>();
            
            SetAxisLimits(default(DateTime), default(DateTime));
            Charting.For<MeasureModel>(MeasureModel.mapper);
            Formatter = MeasureModel.Formatter;

            if (config != null)
            {
                this.SetAxisLimits(config.StartDateTime, config.EndDateTime);
            }

            this.StopSimulationCommand = new RelayCommand(StopSimulation, StopSimulationCanExecute);
            this.SaveSimulatedDataCommand = new RelayCommand(SaveSimulatedData, SaveSimulatedDataCanExecute);
        }

        private bool SaveSimulatedDataCanExecute(object obj)
        {
            return !timer.IsEnabled && !valuesSaved;
        }

        private bool StopSimulationCanExecute(object obj)
        {
            return timer.IsEnabled;
        }

        private void SaveSimulatedData(object obj)
        {
            IRestClient client = new WSClient();
            client.SaveData(ChartValues, config, this.MeasurementDevice.ID.GetValueOrDefault());
            valuesSaved = true;
        }

        private void StopSimulation(object obj)
        {
            this.timer.Stop();
        }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                this.RaisePropertyChanged();
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                this.RaisePropertyChanged();
            }
        }
        public ChartValues<MeasureModel> ChartValues
        {
            set
            {
                this.chartValues = value;
                this.RaisePropertyChanged();
            }
            get
            {
                return this.chartValues;
            }
        }

        public void StartSimulation(SimulatorConfiguration config)
        {
            SetAxisLimits(config.StartDateTime, config.EndDateTime);
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            this.config = config;
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i < config.SimulationSpeed && lastValueDate <= config.EndDateTime ; i++)
            {
                //1.) Generate new MeasureModel
                MeasureModel temp = DataGenerator.CalculateNextValue(this.config, this.lastValueDate, this.lastValue);
                //2.) Add new Model to ChartValues
                this.chartValues.Add(temp);
                //3.) Set lastValue and lastValueDate
                this.lastValue = temp.Value;
                this.lastValueDate = temp.DateTime;
            }

            if(config.StartDateTime > lastValueDate.AddMonths(-1))
            {
                SetAxisLimits(config.StartDateTime, lastValueDate);
            } else
            {
                SetAxisLimits(lastValueDate.AddMonths(-1), lastValueDate);
            }
                

            if(this.lastValueDate > config.EndDateTime)
            {
                timer.Stop();
            }
        }

        private void SetAxisLimits(DateTime start, DateTime current)
        {
            AxisMin = (double)start.Ticks;
            AxisMax = (double)current.Ticks;
        }
    }
}
