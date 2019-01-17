using System.Windows.Controls;
using Wetr.Cockpit.ViewModels;
using Wetr.Server.DAL.DTO;

namespace Wetr.Cockpit.Views
{
    /// <summary>
    /// Interaction logic for MeasurementDeviceDetails.xaml
    /// </summary>
    public partial class MeasurementDeviceDetails : UserControl
    {
        public MeasurementDeviceDetails()
        {
            InitializeComponent();

            MeasurementDeviceVM context = (MeasurementDeviceVM)DataContext;
        }
    }
}
