using System.Windows;
using Wetr.Simulator.DataProvider;
using Wetr.Simulator.Helpers;
using Wetr.Simulator.ViewModels;

namespace Wetr.Simulator.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        public ConfigurationWindow()
        {
            InitializeComponent();
            DataContext = new ConfiguratorVM(new WSClient());

            var mvm = DataContext as ConfiguratorVM;
            mvm.StartSimulation = new RelayCommand(StartSimulation);
        }

        private void StartSimulation(object obj)
        {
            var mvm = DataContext as ConfiguratorVM;
            var config = mvm.BundleConfiguration();
            SimulationWindow sim = new SimulationWindow(config, mvm.SelectedMeasurementDeviceVMs);
            sim.Owner = this;
            sim.Show();
        }
    }
}
