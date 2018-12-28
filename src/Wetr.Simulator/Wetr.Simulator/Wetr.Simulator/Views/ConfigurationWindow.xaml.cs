﻿using System;
using System.Collections.Generic;
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
            DataContext = new ConfiguratorVM(new MockRestClient());

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
