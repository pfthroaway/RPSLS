using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace RPSLS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private enum Element { Rock, Paper, Scissors, Lizard, Spock }

        private string[] Elements = { "", "Rock", "Paper", "Scissors", "Lizard", "Spock" };
        private int _playerWins = 0;
        private int _computerWins = 0;
        private int _tieGames = 0;
        private int _playerSelection = 0;
        private int _computerSelection = 0;
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
        public int PlayerSelection
        {
            get { return _playerSelection; }
            set { _playerSelection = value; OnPropertyChanged("PlayerSelectionString"); }
        }

        /// <summary>The computer's current selection.</summary>
        public int ComputerSelection
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
        public string PlayerWinsString
        {
            get { return "Player Wins: " + _playerWins; }
        }

        /// <summary>Games the computer has won with preceding text.</summary>
        public string ComputerWinsString
        {
            get { return "Computer Wins: " + _computerWins; }
        }

        /// <summary>Games that resulted in a tie with preceding text.</summary>
        public string TieGamesString
        {
            get { return "Tie Games: " + _tieGames; }
        }

        /// <summary>The player's current selection.</summary>
        public string PlayerSelectionString
        {
            get { return Elements[_playerSelection]; }
        }

        /// <summary>The computer's current selection.</summary>
        public string ComputerSelectionString
        {
            get { return Elements[_computerSelection]; }
        }

        #endregion Helper Properties

        /// <summary>Generates a random number between min and max (inclusive).</summary>
        /// <param name="min">Inclusive minimum number</param>
        /// <param name="max">Inclusive maximum number</param>
        /// <returns>Returns randomly generated integer between min and max.</returns>
        internal int GenerateRandomNumber(int min, int max)
        {
            int result;

            if (min < max)
                result = ThreadSafeRandom.ThisThreadsRandom.Next(min, max + 1);
            else
                result = ThreadSafeRandom.ThisThreadsRandom.Next(max, min + 1);

            return result;
        }

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
        /// <param name="elementNumber">Element the player has selected.</param>
        private void Play(int elementNumber)
        {
            PlayerSelection = elementNumber;
            ComputerSelection = GenerateRandomNumber(1, 5);

            switch (PlayerSelection)
            {
                case 1:
                    Rock();
                    break;

                case 2:
                    Paper();
                    break;

                case 3:
                    Scissors();
                    break;

                case 4:
                    Lizard();
                    break;

                case 5:
                    Spock();
                    break;
            }
        }

        /// <summary>The player selects Rock.</summary>
        private void Rock()
        {
            switch (ComputerSelection)
            {
                case 1:
                    Tie();
                    break;

                case 2:
                    Lose("Paper covers rock.");
                    break;

                case 3:
                    Win("Rock smashes scissors.");
                    break;

                case 4:
                    Win("Rock crushes lizard.");
                    break;

                case 5:
                    Lose("Spock vaporizes rock.");
                    break;
            }
        }

        /// <summary>The player selects Paper.</summary>
        private void Paper()
        {
            switch (ComputerSelection)
            {
                case 1:
                    Win("Paper covers rock.");
                    break;

                case 2:
                    Tie();
                    break;

                case 3:
                    Lose("Scissors cuts paper.");
                    break;

                case 4:
                    Lose("Lizard eats paper.");
                    break;

                case 5:
                    Win("Paper disproves Spock.");
                    break;
            }
        }

        /// <summary>The player selects Scissors.</summary>
        private void Scissors()
        {
            switch (ComputerSelection)
            {
                case 1:
                    Lose("Rock smashes scissors.");
                    break;

                case 2:
                    Win("Scissors cuts paper.");
                    break;

                case 3:
                    Tie();
                    break;

                case 4:
                    Win("Scissors decapitate lizard.");
                    break;

                case 5:
                    Lose("Spock smashes scissors.");
                    break;
            }
        }

        /// <summary>The player selects Lizard.</summary>
        private void Lizard()
        {
            switch (ComputerSelection)
            {
                case 1:
                    Lose("Rock crushes lizard.");
                    break;

                case 2:
                    Win("Lizard eats paper.");
                    break;

                case 3:
                    Lose("Scissors decapitate lizard.");
                    break;

                case 4:
                    Tie();
                    break;

                case 5:
                    Win("Lizard poisons Spock.");
                    break;
            }
        }

        /// <summary>The player selects Spock.</summary>
        private void Spock()
        {
            switch (ComputerSelection)
            {
                case 1:
                    Win("Spock vaporizes rock.");
                    break;

                case 2:
                    Lose("Paper disproves Spock.");
                    break;

                case 3:
                    Win("Spock smashes scissors.");
                    break;

                case 4:
                    Lose("Lizard poisons Spock.");
                    break;

                case 5:
                    Tie();
                    break;
            }
        }

        #endregion Gameplay

        #region Button-Click Methods

        private void btnRock_Click(object sender, RoutedEventArgs e)
        {
            Play(1);
        }

        private void btnPaper_Click(object sender, RoutedEventArgs e)
        {
            Play(2);
        }

        private void btnScissors_Click(object sender, RoutedEventArgs e)
        {
            Play(3);
        }

        private void btnLizard_Click(object sender, RoutedEventArgs e)
        {
            Play(4);
        }

        private void btnSpock_Click(object sender, RoutedEventArgs e)
        {
            Play(5);
        }

        #endregion Button-Click Methods

        #region Form-Manipulation Methods

        public MainWindow()
        {
            InitializeComponent();
        }

        private void windowRPSLS_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        private void windowRPSLS_KeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            switch (k)
            {
                case Key.D1:
                case Key.NumPad1:
                    Play(1);
                    break;

                case Key.D2:
                case Key.NumPad2:
                    Play(2);
                    break;

                case Key.D3:
                case Key.NumPad3:
                    Play(3);
                    break;

                case Key.D4:
                case Key.NumPad4:
                    Play(4);
                    break;

                case Key.D5:
                case Key.NumPad5:
                    Play(5);
                    break;
            }
        }

        #endregion Form-Manipulation Methods
    }
}