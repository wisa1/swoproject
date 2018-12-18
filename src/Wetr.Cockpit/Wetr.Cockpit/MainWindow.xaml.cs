using System.Windows;
using System.Windows.Input;
using Wetr.Cockpit.ViewModels;

namespace Wetr.Cockpit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }

        private void ToggleBurger(object sender, MouseButtonEventArgs e)
        {
            this.MenuToggleButton.IsChecked = !this.MenuToggleButton.IsChecked;
        }
    }
}
