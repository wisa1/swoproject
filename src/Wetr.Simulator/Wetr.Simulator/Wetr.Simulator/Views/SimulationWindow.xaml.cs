using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wetr.Simulator.Helpers;
using Wetr.Simulator.ViewModels;

namespace Wetr.Simulator.Views
{
    /// <summary>
    /// Interaction logic for SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow
    {
        public SimulationWindow(SimulatorConfiguration config, ObservableCollection<MeasurementDeviceVM> devices)
        {
            InitializeComponent();
            DataContext = new SimulationVM(config, devices);
        }
    }
}
