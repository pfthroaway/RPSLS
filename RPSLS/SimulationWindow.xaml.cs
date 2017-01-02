using Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RPSLS
{
    /// <summary>Interaction logic for SimulationWindow.xaml</summary>
    public partial class SimulationWindow
    {
        internal MainWindow RefToMainWindow { private get; set; }

        #region Button-Click Methods

        private async void btnSimulate_Click(object sender, RoutedEventArgs e)
        {
            int games = Int32Helper.Parse(txtSimulator.Text);
            bool reset = chkReset.IsChecked != null && chkReset.IsChecked.Value;

            if (games > 0)
            {
                await Task.Factory.StartNew(() => { RefToMainWindow.Simulate(games, reset); });
                CloseWindow();
            }
            else
                new Notification("Please enter a valid number between 1 and 1,000,000.", "RPSLS Simulator", NotificationButtons.OK, this).ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public SimulationWindow()
        {
            InitializeComponent();
            txtSimulator.Focus();
        }

        private void txtSimulator_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.Numbers);
        }

        private void txtSimulator_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Numbers);

            if (Int32Helper.Parse(txtSimulator.Text) > 1000000)
                txtSimulator.Text = 1000000.ToString();
            btnSimulate.IsEnabled = txtSimulator.Text.Length > 0;
        }

        private void txtSimulator_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.TextBoxGotFocus(sender);
        }

        private void windowSimulation_Closing(object sender, CancelEventArgs e)
        {
            RefToMainWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}