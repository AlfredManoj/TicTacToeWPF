namespace TicTacToeWPF.Common
{
    /// <summary>
    /// Class for logging
    /// </summary>
    public class LogReporter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        /// <summary>
        /// constructor
        /// </summary>
        static LogReporter()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Log information
        /// </summary>
        /// <param name="message"></param>
        public static void LogInfo(string message)
        {
            log.Info(message);
        }

        /// <summary>
        /// Log warning
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarn(string message)
        {
            log.Warn(message);
        }

        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="message"></param>
        public static void LogError(string message)
        {
            log.Error(message);
        }
    }
}