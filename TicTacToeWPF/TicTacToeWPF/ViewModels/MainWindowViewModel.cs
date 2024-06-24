using Caliburn.Micro;
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
            }
        }

        public void MarkBlock(object block)
        {
            if (block is Block playedBlock)
            {
                if (!string.IsNullOrEmpty(playedBlock.PlayerIcon))
                {
                    MessageText = Constants.InvalidMove;
                    return;
                }
                playedBlock.PlayerIcon = CurrentPlayer.ToString();
                ChangeCurrentPlayer();
            }
        }

        /// <summary>
        /// Logic for starting new game
        /// </summary>
        public void StartNewGame()
        {
            PlayingBlocks.ForEach(block =>
            {
                block.PlayerIcon = string.Empty;
                block.IsWinningBlock = false;
            });
        }

        /// <summary>
        /// Logic for resetting the game statistics
        /// </summary>
        public void ResetStatistics()
        {
            PlayerOWinCount = PlayerXWinCount = TieCount = 0;
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