using Newtonsoft.Json;
using System.IO;
using TicTacToeWPF.Models;

namespace TicTacToeWPF.Common
{
    public class HistoryDataHelper
    {
        private static readonly string filePath = Path.Combine("C:\\ProgramData", "TicTacToe", "game_stats.json");

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