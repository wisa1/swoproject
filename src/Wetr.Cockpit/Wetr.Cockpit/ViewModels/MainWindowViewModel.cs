using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Cockpit.Views;

namespace Wetr.Cockpit.ViewModels
{
    /*
     * Represents the Entry point to the application
     * This ViewModel basically only holds the user controls, that can be brought up using the burger menu
     */
    public class MainWindowViewModel
    {
        public MainListItem[] ListItems { get; }

        public MainWindowViewModel()
        {
            ListItems = new MainListItem[]
            {
                new MainListItem("Manage Data", new ManageDataView()),
                new MainListItem("Query Data", new QueryDataView())
            };
        }
    }
}
