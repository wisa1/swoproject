using System.Windows.Controls;

namespace Wetr.Cockpit.ViewModels
{
    public class MainListItem : ViewModelBase
    {
        private string name;
        private UserControl userControl;

        public MainListItem(string name, UserControl userControl)
        {
            this.name = name;
            this.userControl = userControl;
        }

        #region Properties
        public string Name
        {
            get { return name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public UserControl UserControl
        {
            get { return userControl; }
            set
            {
                if(this.userControl != value)
                {
                    this.UserControl = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion
    }
}
