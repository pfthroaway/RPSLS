using Extensions;
using Extensions.DataTypeHelpers;
using Extensions.Enums;
using RPSLS.Classes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RPSLS.Views
{
    /// <summary>Interaction logic for SimulationWindow.xaml</summary>
    public partial class SimulationWindow
    {
        #region Button-Click Methods

        private async void BtnSimulate_Click(object sender, RoutedEventArgs e)
        {
            int games = Int32Helper.Parse(TxtSimulator.Text);
            bool reset = ChkReset.IsChecked != null && ChkReset.IsChecked.Value;

            if (games > 0)
            {
                await Task.Factory.StartNew(() => { GameState.GameLogic.Simulate(games, reset); });
                Close();
            }
            else
                GameState.DisplayNotification("Please enter a valid number between 1 and 1,000,000.", "RPSLS Simulator");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) => Close();

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        private void Close() => GameState.GoBack();

        public SimulationWindow()
        {
            InitializeComponent();
            TxtSimulator.Focus();
        }

        private void TxtSimulator_PreviewKeyDown(object sender, KeyEventArgs e) => Functions.PreviewKeyDown(e, KeyType.Integers);

        private void TxtSimulator_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Integers);

            if (Int32Helper.Parse(TxtSimulator.Text) > 1000000)
                TxtSimulator.Text = "1000000";
            BtnSimulate.IsEnabled = TxtSimulator.Text.Length > 0;
        }

        private void TxtSimulator_GotFocus(object sender, RoutedEventArgs e) => Functions.TextBoxGotFocus(sender);

        private void SimulationWindow_OnLoaded(object sender, RoutedEventArgs e) => GameState.CalculateScale(Grid);

        #endregion Page-Manipulation Methods
    }
}