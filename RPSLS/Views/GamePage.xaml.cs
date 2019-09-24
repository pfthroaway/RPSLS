using RPSLS.Classes;
using RPSLS.Enums;
using System.Windows;
using System.Windows.Input;

namespace RPSLS.Views
{
    /// <summary>Interaction logic for GamePage.xaml</summary>
    public partial class GamePage
    {
        #region Button-Click Methods

        private void BtnRock_Click(object sender, RoutedEventArgs e) => GameState.GameLogic.Play(Element.Rock);

        private void BtnPaper_Click(object sender, RoutedEventArgs e) => GameState.GameLogic.Play(Element.Paper);

        private void BtnScissors_Click(object sender, RoutedEventArgs e) => GameState.GameLogic.Play(Element.Scissors);

        private void BtnLizard_Click(object sender, RoutedEventArgs e) => GameState.GameLogic.Play(Element.Lizard);

        private void BtnSpock_Click(object sender, RoutedEventArgs e) => GameState.GameLogic.Play(Element.Spock);

        private void BtnSimulation_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new SimulationWindow());

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        private void Close() => GameState.Close();

        public GamePage()
        {
            InitializeComponent();
            DataContext = GameState.GameLogic;
        }

        private void WindowRPSLS_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1:
                case Key.NumPad1:
                    GameState.GameLogic.Play(Element.Rock);
                    break;

                case Key.D2:
                case Key.NumPad2:
                    GameState.GameLogic.Play(Element.Paper);
                    break;

                case Key.D3:
                case Key.NumPad3:
                    GameState.GameLogic.Play(Element.Scissors);
                    break;

                case Key.D4:
                case Key.NumPad4:
                    GameState.GameLogic.Play(Element.Lizard);
                    break;

                case Key.D5:
                case Key.NumPad5:
                    GameState.GameLogic.Play(Element.Spock);
                    break;

                case Key.Escape:
                    Close();
                    break;
            }
        }

        private void GamePage_OnLoaded(object sender, RoutedEventArgs e) => BtnRock.Focus();

        #endregion Window-Manipulation Methods
    }
}