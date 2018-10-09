using N3PR_WPFclient.ViewModels;
using System.Windows;

namespace N3PR_WPFclient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _mainViewModel = new MainWindowViewModel();
            this.DataContext = _mainViewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Closing Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                // Signal write to view-model
                _mainViewModel.TerminateExecutionAndSaveUnsentData();
                // Close the window
                Application.Current.Shutdown();
            }
            else
            {
                // Keep alive
                e.Cancel = true;
            }
        }
    }
}
