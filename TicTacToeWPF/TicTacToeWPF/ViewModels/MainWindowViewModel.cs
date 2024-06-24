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
        private Players currentPlayer;
        private List<Block> playingBlocks;

        /// <summary>
        /// constructor
        /// </summary>
        public MainWindowViewModel()
        {
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

        /// <summary>
        /// Logic for starting new game
        /// </summary>
        public void StartNewGame()
        {
        }

        /// <summary>
        /// Logic for resetting the game statistics
        /// </summary>
        public void ResetStatistics()
        {
        }

        private void AddPlayingBlocks()
        {
            for (int blockId = 0; blockId < 9; blockId++)
            {
                PlayingBlocks.Add(new Block { Id = blockId });
            }
        }
    }
}