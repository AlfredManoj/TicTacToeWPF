using Newtonsoft.Json;
using System.IO;
using TicTacToeWPF.Models;

namespace TicTacToeWPF.Common
{
    /// <summary>
    /// Helper class for persistent history
    /// </summary>
    public class HistoryDataHelper
    {
        private static readonly string filePath = Path.Combine("C:\\ProgramData", "TicTacToe", "game_stats.json");

        /// <summary>
        /// Loads the history
        /// </summary>
        /// <returns></returns>
        public static HistoryData Load()
        {
            if (!File.Exists(filePath))
            {
                return new HistoryData();
            }

            var json = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<HistoryData>(json);
            return data ?? new HistoryData();
        }

        /// <summary>
        /// Saves history data
        /// </summary>
        /// <param name="currentStatistics"></param>
        public static void Save(HistoryData currentStatistics)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonConvert.SerializeObject(currentStatistics, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}