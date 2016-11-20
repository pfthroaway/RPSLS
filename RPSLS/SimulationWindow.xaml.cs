using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RPSLS
{
    /// <summary>
    /// Interaction logic for SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window
    {
        internal MainWindow RefToMainWindow { get; set; }

        /// <summary>Turns several Keyboard.Keys into a list of Keys which can be tested using List.Any.</summary>
        /// <param name="keys">Array of Keys</param>
        /// <returns>List of Keys' KeyDown status</returns>
        internal static List<bool> GetListOfKeys(params Key[] keys)
        {
            List<bool> allKeys = new List<bool>();
            foreach (Key key in keys)
                allKeys.Add(Keyboard.IsKeyDown(key));
            return allKeys;
        }

        /// <summary>Utilizes int.TryParse to easily Parse an Integer.</summary>
        /// <param name="text">Text to be parsed</param>
        /// <returns>Parsed integer</returns>
        internal static int Parse(string text)
        {
            int temp = 0;
            int.TryParse(text, out temp);
            return temp;
        }

        #region Button-Click Methods

        private async void btnSimulate_Click(object sender, RoutedEventArgs e)
        {
            int games = Parse(txtSimulator.Text);
            bool reset = chkReset.IsChecked.Value;

            if (games > 0)
            {
                await Task.Factory.StartNew(() => { RefToMainWindow.Simulate(games, reset); });
                CloseWindow();
            }
            else
                MessageBox.Show("Please enter a valid number between 1 and 1,000,000.", "RPSLS Simulator", MessageBoxButton.OK);
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

        private void windowSimulation_Closing(object sender, CancelEventArgs e)
        {
            RefToMainWindow.Show();
        }

        private void txtSimulator_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            List<bool> keys = GetListOfKeys(Key.Back, Key.Delete, Key.Home, Key.End, Key.Enter, Key.Tab, Key.LeftAlt, Key.RightAlt, Key.Left, Key.Right, Key.LeftCtrl, Key.RightCtrl, Key.Escape);

            if (keys.Any(key => key == true) || (Key.D0 <= k && k <= Key.D9) || (Key.NumPad0 <= k && k <= Key.NumPad9))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtSimulator_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtSimulator.Text = new string((from c in txtSimulator.Text
                                            where char.IsDigit(c)
                                            select c).ToArray());
            txtSimulator.CaretIndex = txtSimulator.Text.Length;

            if (Parse(txtSimulator.Text) > 1000000)
                txtSimulator.Text = 1000000.ToString();
            if (txtSimulator.Text.Length > 0)
                btnSimulate.IsEnabled = true;
            else
                btnSimulate.IsEnabled = false;
        }

        private void txtSimulator_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSimulator.SelectAll();
        }

        #endregion Window-Manipulation Methods
    }
}