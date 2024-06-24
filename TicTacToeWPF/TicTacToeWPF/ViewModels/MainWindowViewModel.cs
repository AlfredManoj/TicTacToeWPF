using Caliburn.Micro;
using System.Windows;
using TicTacToeWPF.Common;
using TicTacToeWPF.Models;

namespace TicTacToeWPF
{
    /// <summary>
    /// ViewModel class for Mainwindow
    /// </summary>
    public class MainWindowViewModel : Screen
    {
        private string messageText;
        private bool isMessageShown;
        private int playerXWinCount;
        private int playerOWinCount;
        private int tieCount;
        private Players currentPlayer;
        private List<Block> playingBlocks;
        private bool isGameInProgress;

        /// <summary>
        /// constructor
        /// </summary>
        public MainWindowViewModel()
        {
            CurrentPlayer = Players.X;
            PlayingBlocks = new List<Block>();
            AddPlayingBlocks();
        }

        public Players CurrentPlayer
        {
            get => currentPlayer;
            set => Set(ref currentPlayer, value);
        }

        public List<Block> PlayingBlocks
        {
            get => playingBlocks;
            set => Set(ref playingBlocks, value);
        }

        public bool IsMessageShown
        {
            get => isMessageShown;
            set => Set(ref isMessageShown, value);
        }

        public bool IsGameInProgress
        {
            get => isGameInProgress;
            set => Set(ref isGameInProgress, value);
        }

        public int PlayerXWinCount
        {
            get => playerXWinCount;
            set => Set(ref playerXWinCount, value);
        }

        public int PlayerOWinCount
        {
            get => playerOWinCount;
            set => Set(ref playerOWinCount, value);
        }

        public int TieCount
        {
            get => tieCount;
            set => Set(ref tieCount, value);
        }

        public string MessageText
        {
            get => messageText;
            set
            {
                Set(ref messageText, value);
                if (!string.IsNullOrEmpty(value))
                {
                    IsMessageShown = true;
                }
                LogReporter.LogInfo(value);
            }
        }

        public void MarkBlock(object block)
        {
            if (block is Block playedBlock)
            {
                IsGameInProgress = true;
                if (!string.IsNullOrEmpty(playedBlock.PlayerIcon))
                {
                    MessageText = Constants.InvalidMove;
                    return;
                }
                playedBlock.PlayerIcon = CurrentPlayer.ToString();
                CalculateGameStatus();
                ChangeCurrentPlayer();
            }
        }

        /// <summary>
        /// Logic for starting new game
        /// </summary>
        public void StartNewGame()
        {
            if (IsGameInProgress)
            {
                var result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    MessageText = "Game abandoned";
                }
            }
            PlayingBlocks.ForEach(block =>
            {
                block.PlayerIcon = string.Empty;
                block.IsWinningBlock = false;
                block.IsBlockEnabled = true;
            });
            IsGameInProgress = false;
            LogReporter.LogInfo("New game started");
        }

        /// <summary>
        /// Logic for resetting the game statistics
        /// </summary>
        public void ResetStatistics()
        {
            PlayerOWinCount = PlayerXWinCount = TieCount = 0;
            LogReporter.LogInfo("Game statistics reset");
        }

        private void CalculateGameStatus()
        {
            if (GameStatusChecker.CheckWin(PlayingBlocks))
            {
                PlayingBlocks.ForEach(block => block.IsBlockEnabled = false);
                if (CurrentPlayer == Players.X)
                {
                    PlayerXWinCount++;
                    MessageText = Constants.PlayerXWin;
                }
                else
                {
                    PlayerOWinCount++;
                    MessageText = Constants.PlayerOWin;
                }
                IsGameInProgress = false;
            }
            else if (GameStatusChecker.CheckTie(PlayingBlocks))
            {
                PlayingBlocks.ForEach(block => block.IsBlockEnabled = false);
                TieCount++;
                MessageText = Constants.Tie;
                IsGameInProgress = false;
            }
        }

        private void AddPlayingBlocks()
        {
            for (int blockId = 0; blockId < 9; blockId++)
            {
                PlayingBlocks.Add(new Block { Id = blockId });
            }
        }

        private void ChangeCurrentPlayer()
        {
            CurrentPlayer = CurrentPlayer == Players.X ? Players.O : Players.X;
        }
    }
}