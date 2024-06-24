using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeWPF.Common;

namespace TicTacToeWPF.Models
{
    public class Block : PropertyChangedBase
    {
        private string? playerIcon;
        private bool isWinningBlock;

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
    }
}
