using Extensions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RPSLS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public enum Element { Rock, Paper, Scissors, Lizard, Spock }

        private int _playerWins;
        private int _computerWins;
        private int _tieGames;
        private Element? _playerSelection;
        private Element? _computerSelection;
        private string _result = "";

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Updates data-binding when a Property has changed.
        /// </summary>
        /// <param name="property"></param>
        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>Games the player has won.</summary>
        public int PlayerWins
        {
            get { return _playerWins; }
            set { _playerWins = value; OnPropertyChanged("PlayerWinsString"); }
        }

        /// <summary>Games the computer has won.</summary>
        public int ComputerWins
        {
            get { return _computerWins; }
            set { _computerWins = value; OnPropertyChanged("ComputerWinsString"); }
        }

        /// <summary>Games that resulted in a tie.</summary>
        public int TieGames
        {
            get { return _tieGames; }
            set { _tieGames = value; OnPropertyChanged("TieGamesString"); }
        }

        /// <summary>The player's current selection.</summary>
        public Element? PlayerSelection
        {
            get { return _playerSelection; }
            set { _playerSelection = value; OnPropertyChanged("PlayerSelectionString"); }
        }

        /// <summary>The computer's current selection.</summary>
        public Element? ComputerSelection
        {
            get { return _computerSelection; }
            set { _computerSelection = value; OnPropertyChanged("ComputerSelectionString"); }
        }

        /// <summary>The result of the current game.</summary>
        public string Result
        {
            get { return _result; }
            set { _result = value; OnPropertyChanged("Result"); }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Games the player has won with preceding text.</summary>
        public string PlayerWinsString => "Player Wins: " + PlayerWins.ToString("N0");

        /// <summary>Games the computer has won with preceding text.</summary>
        public string ComputerWinsString => "Computer Wins: " + ComputerWins.ToString("N0");

        /// <summary>Games that resulted in a tie with preceding text.</summary>
        public string TieGamesString => "Tie Games: " + TieGames.ToString("N0");

        /// <summary>The player's current selection.</summary>
        public string PlayerSelectionString => PlayerSelection.ToString();

        /// <summary>The computer's current selection.</summary>
        public string ComputerSelectionString => ComputerSelection.ToString();

        #endregion Helper Properties

        #region Game Results

        /// <summary>The game resulted in a win for the player.</summary>
        /// <param name="result">Text to be displayed</param>
        private void Win(string result)
        {
            PlayerWins += 1;
            Result = result + " You win!";
        }

        /// <summary>The game resulted in a loss for the player.</summary>
        /// <param name="result">Text to be displayed</param>
        private void Lose(string result)
        {
            ComputerWins += 1;
            Result = result + " You lose.";
        }

        /// <summary>The game resulted in tie.</summary>
        private void Tie()
        {
            TieGames += 1;
            Result = "Tie game.";
        }

        #endregion Game Results

        #region Gameplay

        /// <summary>Starts a new round.</summary>
        /// <param name="selectedElement">Element the player has selected.</param>
        private void Play(Element selectedElement)
        {
            PlayerSelection = selectedElement;
            ComputerSelection = (Element)Functions.GenerateRandomNumber(0, 4);

            switch (PlayerSelection)
            {
                case Element.Rock:
                    Rock();
                    break;

                case Element.Paper:
                    Paper();
                    break;

                case Element.Scissors:
                    Scissors();
                    break;

                case Element.Lizard:
                    Lizard();
                    break;

                case Element.Spock:
                    Spock();
                    break;
            }
        }

        /// <summary>Simulates an amount of games.</summary>
        /// <param name="games">Number of games to simulate</param>
        /// <param name="resetScore">Reset current score?</param>
        internal async void Simulate(int games, bool resetScore)
        {
            if (resetScore)
                ResetScore();
            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < games; i++)
                    Play((Element)Functions.GenerateRandomNumber(0, 4));
            });
        }

        /// <summary>Resets all wins/losses/ties.</summary>
        private void ResetScore()
        {
            PlayerWins = 0;
            ComputerWins = 0;
            TieGames = 0;
        }

        /// <summary>The player selects Rock.</summary>
        private void Rock()
        {
            switch (ComputerSelection)
            {
                case Element.Rock:
                    Tie();
                    break;

                case Element.Paper:
                    Lose("Paper covers rock.");
                    break;

                case Element.Scissors:
                    Win("Rock smashes scissors.");
                    break;

                case Element.Lizard:
                    Win("Rock crushes lizard.");
                    break;

                case Element.Spock:
                    Lose("Spock vaporizes rock.");
                    break;
            }
        }

        /// <summary>The player selects Paper.</summary>
        private void Paper()
        {
            switch (ComputerSelection)
            {
                case Element.Rock:
                    Win("Paper covers rock.");
                    break;

                case Element.Paper:
                    Tie();
                    break;

                case Element.Scissors:
                    Lose("Scissors cuts paper.");
                    break;

                case Element.Lizard:
                    Lose("Lizard eats paper.");
                    break;

                case Element.Spock:
                    Win("Paper disproves Spock.");
                    break;
            }
        }

        /// <summary>The player selects Scissors.</summary>
        private void Scissors()
        {
            switch (ComputerSelection)
            {
                case Element.Rock:
                    Lose("Rock smashes scissors.");
                    break;

                case Element.Paper:
                    Win("Scissors cuts paper.");
                    break;

                case Element.Scissors:
                    Tie();
                    break;

                case Element.Lizard:
                    Win("Scissors decapitate lizard.");
                    break;

                case Element.Spock:
                    Lose("Spock smashes scissors.");
                    break;
            }
        }

        /// <summary>The player selects Lizard.</summary>
        private void Lizard()
        {
            switch (ComputerSelection)
            {
                case Element.Rock:
                    Lose("Rock crushes lizard.");
                    break;

                case Element.Paper:
                    Win("Lizard eats paper.");
                    break;

                case Element.Scissors:
                    Lose("Scissors decapitate lizard.");
                    break;

                case Element.Lizard:
                    Tie();
                    break;

                case Element.Spock:
                    Win("Lizard poisons Spock.");
                    break;
            }
        }

        /// <summary>The player selects Spock.</summary>
        private void Spock()
        {
            switch (ComputerSelection)
            {
                case Element.Rock:
                    Win("Spock vaporizes rock.");
                    break;

                case Element.Paper:
                    Lose("Paper disproves Spock.");
                    break;

                case Element.Scissors:
                    Win("Spock smashes scissors.");
                    break;

                case Element.Lizard:
                    Lose("Lizard poisons Spock.");
                    break;

                case Element.Spock:
                    Tie();
                    break;
            }
        }

        #endregion Gameplay

        #region Button-Click Methods

        private void btnRock_Click(object sender, RoutedEventArgs e)
        {
            Play(Element.Rock);
        }

        private void btnPaper_Click(object sender, RoutedEventArgs e)
        {
            Play(Element.Paper);
        }

        private void btnScissors_Click(object sender, RoutedEventArgs e)
        {
            Play(Element.Scissors);
        }

        private void btnLizard_Click(object sender, RoutedEventArgs e)
        {
            Play(Element.Lizard);
        }

        private void btnSpock_Click(object sender, RoutedEventArgs e)
        {
            Play(Element.Spock);
        }

        private void btnSimulation_Click(object sender, RoutedEventArgs e)
        {
            SimulationWindow simulationWindow = new SimulationWindow { RefToMainWindow = this };
            simulationWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        #endregion Button-Click Methods

        #region Form-Manipulation Methods

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void windowRPSLS_KeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            switch (k)
            {
                case Key.D1:
                case Key.NumPad1:
                    Play(Element.Rock);
                    break;

                case Key.D2:
                case Key.NumPad2:
                    Play(Element.Paper);
                    break;

                case Key.D3:
                case Key.NumPad3:
                    Play(Element.Scissors);
                    break;

                case Key.D4:
                case Key.NumPad4:
                    Play(Element.Lizard);
                    break;

                case Key.D5:
                case Key.NumPad5:
                    Play(Element.Spock);
                    break;

                case Key.Escape:
                    this.Close();
                    break;
            }
        }

        #endregion Form-Manipulation Methods
    }
}