using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RPSLS_WPF 
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        string[] Elements = { "Rock", "Paper", "Scissors", "Lizard", "Spock" };
        int _playerWins = 0;
        int _computerWins = 0;
        int _tieGames = 0;
        int _playerSelection = 0;
        int _computerSelection = 0;
        string _result = "";

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        #region Get/Set
        public int PlayerWins
        {
            get { return _playerWins; }
            set { _playerWins = value; OnPropertyChanged("PlayerWinsString"); }
        }

        public string PlayerWinsString
        {
            get { return "Player Wins: " + _playerWins; }
        }

        public int ComputerWins
        {
            get { return _computerWins; }
            set { _computerWins = value; OnPropertyChanged("ComputerWinsString"); }
        }

        public string ComputerWinsString
        {
            get { return "Computer Wins: " + _computerWins; }
        }

        public int TieGames
        {
            get { return _tieGames; }
            set { _tieGames = value; OnPropertyChanged("TieGamesString"); }
        }

        public string TieGamesString
        {
            get { return "Tie Games: " + _tieGames; }
        }

        public int PlayerSelection
        {
            get { return _playerSelection; }
            set { _playerSelection = value; OnPropertyChanged("PlayerSelectionString"); }
        }

        public string PlayerSelectionString
        {
            get { return Elements[_playerSelection]; }
        }

        public int ComputerSelection
        {
            get { return _computerSelection; }
            set { _computerSelection = value; OnPropertyChanged("ComputerSelectionString"); }
        }

        public string ComputerSelectionString
        {
            get { return Elements[_computerSelection]; }
        }

        public string Result
        {
            get { return _result; }
            set { _result = value; OnPropertyChanged("Result"); }
        }
        #endregion


        /// <summary>
        /// Generates a random number between min and max (inclusive).
        /// </summary>
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
        private void Win(string result)
        {
            PlayerWins += 1;
            Result = result + " You win!";
        }

        private void Lose(string result)
        {
            ComputerWins += 1;
            Result = result + " You lose.";
        }

        private void Tie()
        {
            TieGames += 1;
            Result = "Tie game.";
        }
        #endregion

        #region Gameplay
        private void Play(int elementNumber)
        {
            PlayerSelection = elementNumber;
            ComputerSelection = GenerateRandomNumber(0, 4);

            switch (PlayerSelection)
            {
                case 0:
                    Rock();
                    break;
                case 1:
                    Paper();
                    break;
                case 2:
                    Scissors();
                    break;
                case 3:
                    Lizard();
                    break;
                case 4:
                    Spock();
                    break;
            }
        }

        private void Rock()
        {
            switch (ComputerSelection)
            {
                case 0:
                    Tie();
                    break;
                case 1:
                    Lose("Paper covers rock.");
                    break;
                case 2:
                    Win("Rock smashes scissors.");
                    break;
                case 3:
                    Win("Rock crushes lizard.!");
                    break;
                case 4:
                    Lose("Spock vaporizes rock.");
                    break;
            }
        }

        private void Paper()
        {
            switch (ComputerSelection)
            {
                case 0:
                    Win("Paper covers rock.");
                    break;
                case 1:
                    Tie();
                    break;
                case 2:
                    Lose("Scissors cuts paper.");
                    break;
                case 3:
                    Lose("Lizard eats paper.");
                    break;
                case 4:
                    Win("Paper disproves Spock.");
                    break;
            }
        }

        private void Scissors()
        {
            switch (ComputerSelection)
            {
                case 0:
                    Lose("Rock smashes scissors.");
                    break;
                case 1:
                    Win("Scissors cuts paper.");
                    break;
                case 2:
                    Tie();
                    break;
                case 3:
                    Win("Scissors decapitate lizard.");
                    break;
                case 4:
                    Lose("Spock smashes scissors.");
                    break;
            }
        }

        private void Lizard()
        {
            switch (ComputerSelection)
            {
                case 0:
                    Lose("Rock crushes lizard.");
                    break;
                case 1:
                    Win("Lizard eats paper.");
                    break;
                case 2:
                    Lose("Scissors decapitate lizard.");
                    break;
                case 3:
                    Tie();
                    break;
                case 4:
                    Win("Lizard poisons Spock.");
                    break;
            }
        }

        private void Spock()
        {
            switch (ComputerSelection)
            {
                case 0:
                    Win("Spock vaporizes rock.");
                    break;
                case 1:
                    Lose("Paper disproves Spock.");
                    break;
                case 2:
                    Win("Spock smashes scissors.");
                    break;
                case 3:
                    Lose("Lizard poisons Spock.");
                    break;
                case 4:
                    Tie();
                    break;
            }
        }
        #endregion

        #region Button-Click Methods
        private void btnRock_Click(object sender, RoutedEventArgs e)
        {
            Play(0);
        }

        private void btnPaper_Click(object sender, RoutedEventArgs e)
        {
            Play(1);
        }

        private void btnScissors_Click(object sender, RoutedEventArgs e)
        {
            Play(2);
        }

        private void btnLizard_Click(object sender, RoutedEventArgs e)
        {
            Play(3);
        }

        private void btnSpock_Click(object sender, RoutedEventArgs e)
        {
            Play(4);
        }
        #endregion


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        internal static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}