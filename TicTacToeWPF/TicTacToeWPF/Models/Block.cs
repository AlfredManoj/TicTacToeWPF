using Caliburn.Micro;

namespace TicTacToeWPF.Models
{
    /// <summary>
    /// DTO class for each game block
    /// </summary>
    public class Block : PropertyChangedBase
    {
        private string? playerIcon;
        private bool isWinningBlock;
        private bool isBlockEnabled = true;

        public int Id { get; set; }

        public string? PlayerIcon
        {
            get => playerIcon;
            set => Set(ref playerIcon, value);
        }

        public bool IsWinningBlock
        {
            get => isWinningBlock;
            set => Set(ref isWinningBlock, value);
        }

        public bool IsBlockEnabled
        {
            get => isBlockEnabled;
            set => Set(ref isBlockEnabled, value);
        }
    }
}