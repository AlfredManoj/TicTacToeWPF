using Caliburn.Micro;
using System.Windows;
using TicTacToeWPF.Common;
using TicTacToeWPF.Models;
using TicTacToeWPF.Views;

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
        private HistoryData historyData;
        private Players currentPlayer;
        private List<Block> playingBlocks;
        private bool isGameInProgress;

        /// <summary>
        /// constructor
        /// </summary>
        public MainWindowViewModel()
        {
            try
            {
                CurrentPlayer = Players.X;
                PlayingBlocks = new List<Block>();
                AddPlayingBlocks();
                historyData = HistoryDataHelper.Load();
            }
            catch (Exception ex)
            {
                LogReporter.LogError(ex.Message);
            }
        }

        #region Public Properties

        /// <summary>
        /// Holds historical data
        /// </summary>
        public HistoryData HistoryData
        { get { return historyData; } }

        /// <summary>
        /// Determines the current player 
        /// </summary>
        public Players CurrentPlayer
        {
            get => currentPlayer;
            set => Set(ref currentPlayer, value);
        }

        /// <summary>
        /// Holds information for each playing block
        /// </summary>
        public List<Block> PlayingBlocks
        {
            get => playingBlocks;
            set => Set(ref playingBlocks, value);
        }

        /// <summary>
        /// Determines if there is some message to be shown to user
        /// </summary>
        public bool IsMessageShown
        {
            get => isMessageShown;
            set => Set(ref isMessageShown, value);
        }

        /// <summary>
        /// Is a game is in progress
        /// </summary>
        public bool IsGameInProgress
        {
            get => isGameInProgress;
            set => Set(ref isGameInProgress, value);
        }

        /// <summary>
        /// No of times player X has won in the current instance
        /// </summary>
        public int PlayerXWinCount
        {
            get => playerXWinCount;
            set => Set(ref playerXWinCount, value);
        }

        /// <summary>
        /// No of times player O has won in the current instance
        /// </summary>
        public int PlayerOWinCount
        {
            get => playerOWinCount;
            set => Set(ref playerOWinCount, value);
        }

        /// <summary>
        /// No of times there is a tie in the current instance
        /// </summary>
        public int TieCount
        {
            get => tieCount;
            set => Set(ref tieCount, value);
        }

        /// <summary>
        /// Message to be shown to the user
        /// </summary>
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

        #endregion Public Properties

        /// <summary>
        /// Marks the played block
        /// </summary>
        /// <param name="block"></param>
        public void MarkBlock(object block)
        {
            try
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
            catch (Exception ex)
            {
                LogReporter.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Logic for starting new game
        /// </summary>
        public void StartNewGame()
        {
            try
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
                        LogReporter.LogWarn("Game abandoned");
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
            catch (Exception ex)
            {
                LogReporter.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Logic for resetting the game statistics
        /// </summary>
        public void ResetStatistics()
        {
            try
            {
                PlayerOWinCount = PlayerXWinCount = TieCount = 0;
                LogReporter.LogInfo("Game statistics reset");
            }
            catch (Exception ex)
            {
                LogReporter.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Shows the history window
        /// </summary>
        public void ShowHistory()
        {
            try
            {
                var historyWindow = new History();
                historyWindow.DataContext = this;
                historyWindow.ShowActivated = true;
                historyWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                LogReporter.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Calculates the current game status after each move
        /// </summary>
        private void CalculateGameStatus()
        {
            if (GameStatusChecker.CheckWin(PlayingBlocks))
            {
                PlayingBlocks.ForEach(block => block.IsBlockEnabled = false);
                if (CurrentPlayer == Players.X)
                {
                    PlayerXWinCount++;
                    historyData.WinsX++;
                    MessageText = Constants.PlayerXWin;
                }
                else
                {
                    PlayerOWinCount++;
                    historyData.WinsO++;
                    MessageText = Constants.PlayerOWin;
                }
                IsGameInProgress = false;
            }
            else if (GameStatusChecker.CheckTie(PlayingBlocks))
            {
                PlayingBlocks.ForEach(block => block.IsBlockEnabled = false);
                TieCount++;
                historyData.Ties++;
                MessageText = Constants.Tie;
                IsGameInProgress = false;
            }
            if (!IsGameInProgress)
            {
                HistoryDataHelper.Save(historyData);
            }
        }

        /// <summary>
        /// Add playing blocks to the game
        /// </summary>
        private void AddPlayingBlocks()
        {
            for (int blockId = 0; blockId < 9; blockId++)
            {
                PlayingBlocks.Add(new Block { Id = blockId });
            }
        }

        /// <summary>
        /// Changes the current player
        /// </summary>
        private void ChangeCurrentPlayer()
        {
            CurrentPlayer = CurrentPlayer == Players.X ? Players.O : Players.X;
        }
    }
}