using System.Windows.Controls;
using Wetr.Cockpit.ViewModels;
using Wetr.Server.BL.Implementation;

namespace Wetr.Cockpit.Views
{
    public partial class QueryDataView : UserControl
    {
        public QueryDataView()
        {
            InitializeComponent();
            DataContext = new QueryDataVM(new MasterdataManager(), new MeasurementManager());
        }
    }
}
