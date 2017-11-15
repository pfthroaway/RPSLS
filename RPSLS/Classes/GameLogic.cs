using Extensions;
using RPSLS.Enums;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RPSLS.Classes
{
    /// <summary>Represents the logic of the game.</summary>
    internal class GameLogic : INotifyPropertyChanged
    {
        private int _playerWins, _computerWins, _tieGames;
        private Element? _playerSelection, _computerSelection;
        private string _result = "";

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Updates data-binding when a Property has changed.</summary>
        /// <param name="property">Name of Property</param>
        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>Games the player has won.</summary>
        public int PlayerWins
        {
            get => _playerWins;
            set { _playerWins = value; OnPropertyChanged("PlayerWinsString"); }
        }

        /// <summary>Games the computer has won.</summary>
        public int ComputerWins
        {
            get => _computerWins;
            set { _computerWins = value; OnPropertyChanged("ComputerWinsString"); }
        }

        /// <summary>Games that resulted in a tie.</summary>
        public int TieGames
        {
            get => _tieGames;
            set { _tieGames = value; OnPropertyChanged("TieGamesString"); }
        }

        /// <summary>The player's current selection.</summary>
        public Element? PlayerSelection
        {
            get => _playerSelection;
            set { _playerSelection = value; OnPropertyChanged("PlayerSelectionString"); }
        }

        /// <summary>The computer's current selection.</summary>
        public Element? ComputerSelection
        {
            get => _computerSelection;
            set { _computerSelection = value; OnPropertyChanged("ComputerSelectionString"); }
        }

        /// <summary>The result of the current game.</summary>
        public string Result
        {
            get => _result;
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
        internal void Play(Element selectedElement)
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
    }
}