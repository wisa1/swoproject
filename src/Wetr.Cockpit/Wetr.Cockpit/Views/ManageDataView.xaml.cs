using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wetr.Cockpit.ViewModels;
using Wetr.Server.BL.Implementation;

namespace Wetr.Cockpit.Views
{
    /// <summary>
    /// Interaction logic for ManageDataView.xaml
    /// </summary>
    public partial class ManageDataView : UserControl
    {
        public ManageDataView()
        {
            InitializeComponent();

            DataContext = new ManageDataViewModel(new MasterdataManager());
        }
    }
}
