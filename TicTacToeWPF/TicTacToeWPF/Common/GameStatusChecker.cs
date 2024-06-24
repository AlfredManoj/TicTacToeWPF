using TicTacToeWPF.Models;

namespace TicTacToeWPF.Common
{
    /// <summary>
    /// Logic for game win status
    /// </summary>
    public class GameStatusChecker
    {
        /// <summary>
        /// Calculates if any player is won
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public static bool CheckWin(List<Block> blocks)
        {
            if (blocks == null || blocks.Count != 9)
            {
                return false;
            }

            int[,] winningBlockCombinations = new int[,]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 },
                { 0, 3, 6 },
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 0, 4, 8 },
                { 2, 4, 6 }
            };

            for (int combinationCount = 0; combinationCount < 8; combinationCount++)
            {
                int firstBlock = winningBlockCombinations[combinationCount, 0];
                int secondBlock = winningBlockCombinations[combinationCount, 1];
                int thirdBlock = winningBlockCombinations[combinationCount, 2];

                if (!string.IsNullOrEmpty(blocks[firstBlock].PlayerIcon) &&
                    blocks[firstBlock].PlayerIcon == blocks[secondBlock].PlayerIcon &&
                    blocks[firstBlock].PlayerIcon == blocks[thirdBlock].PlayerIcon)
                {
                    blocks[firstBlock].IsWinningBlock = true;
                    blocks[secondBlock].IsWinningBlock = true;
                    blocks[thirdBlock].IsWinningBlock = true;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Logic to check for tie
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public static bool CheckTie(List<Block> blocks)
        {
            if (blocks == null || blocks.Count != 9)
            {
                return false;
            }
            return !blocks.Any(t => string.IsNullOrEmpty(t.PlayerIcon));
        }
    }
}