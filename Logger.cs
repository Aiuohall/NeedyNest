using System;
using System.IO;

namespace NeedyNest
{
    /// <summary>
    /// Lightweight file logger. Writes to <c>&lt;app&gt;\logs\error-yyyyMMdd.log</c>.
    /// Logging is best-effort and never throws.
    /// </summary>
    internal static class Logger
    {
        private static readonly string LogDir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

        public static void Log(Exception ex, string context = null)
        {
            Write($"{(string.IsNullOrEmpty(context) ? "" : context + Environment.NewLine)}{ex}");
        }

        public static void Log(string message) => Write(message);

        private static void Write(string text)
        {
            try
            {
                Directory.CreateDirectory(LogDir);
                string file = Path.Combine(LogDir, $"error-{DateTime.Now:yyyyMMdd}.log");
                string entry =
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {text}{Environment.NewLine}" +
                    new string('-', 70) + Environment.NewLine;
                File.AppendAllText(file, entry);
            }
            catch
            {
                // A logger must never crash the app.
            }
        }
    }
}
