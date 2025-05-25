using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace ORCLE_CK.Utils
{
    internal class Logger
    {
        private static readonly string logPath;
        private static readonly bool enableLogging;
        private static readonly object lockObject = new object();

        static Logger()
        {
            logPath = ConfigurationManager.AppSettings["LogPath"] ?? "Logs\\";
            enableLogging = bool.Parse(ConfigurationManager.AppSettings["EnableLogging"] ?? "true");

            if (enableLogging && !Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
        }

        public static void Initialize()
        {
            if (enableLogging)
            {
                LogInfo("Logger initialized");
            }
        }

        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public static void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        public static void LogError(string message, Exception? exception = null)
        {
            var fullMessage = exception != null ? $"{message}\nException: {exception}" : message;
            Log("ERROR", fullMessage);
        }

        public static void LogDebug(string message)
        {
            Log("DEBUG", message);
        }

        private static void Log(string level, string message)
        {
            if (!enableLogging) return;

            try
            {
                lock (lockObject)
                {
                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var logEntry = $"[{timestamp}] [{level}] {message}";

                    var fileName = $"log_{DateTime.Now:yyyyMMdd}.txt";
                    var filePath = Path.Combine(logPath, fileName);

                    File.AppendAllText(filePath, logEntry + Environment.NewLine);
                }
            }
            catch
            {
                // Ignore logging errors to prevent application crashes
            }
        }

        public static void ClearOldLogs(int daysToKeep = 30)
        {
            if (!enableLogging) return;

            try
            {
                var cutoffDate = DateTime.Now.AddDays(-daysToKeep);
                var logFiles = Directory.GetFiles(logPath, "log_*.txt");

                foreach (var file in logFiles)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.CreationTime < cutoffDate)
                    {
                        File.Delete(file);
                    }

                }
            }
            catch (Exception ex)
            {
                LogError($"Error clearing old logs: {ex.Message}", ex);
            }
        }
    }
}
