using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IntraDotNet.AspNetCore.Wasm.BrowserConsole
{
    /// <summary>
    /// A logger that logs messages to the browser console.
    /// </summary>
    public class BrowserConsoleLogger : ILogger
    {
        private readonly IConfiguration _configuration;
        private readonly LogLevel _logLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserConsoleLogger"/> class.
        /// </summary>
        /// <param name="logLevel">The minimum log level for messages to be logged.</param>
        public BrowserConsoleLogger(IConfiguration configuration)
        {
            // Use the configuration as needed
            _configuration = configuration;
            _logLevel = configuration.GetValue<LogLevel>("Logging:LogLevel:Default");
        }

        /// <summary>
        /// Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState">The type of the state to begin scope for.</typeparam>
        /// <param name="state">The identifier for the scope.</param>
        /// <returns>An IDisposable that ends the logical operation scope on dispose.</returns>
        IDisposable ILogger.BeginScope<TState>(TState state) => default!;

        /// <summary>
        /// Checks if the given log level is enabled.
        /// </summary>
        /// <param name="logLevel">The log level to check.</param>
        /// <returns>True if the log level is enabled; otherwise, false.</returns>
        public bool IsEnabled(LogLevel logLevel) => logLevel >= _logLevel;

        /// <summary>
        /// Logs a message to the browser console.
        /// </summary>
        /// <typeparam name="TState">The type of the state to log.</typeparam>
        /// <param name="logLevel">The log level of the message.</param>
        /// <param name="eventId">The event ID of the log message.</param>
        /// <param name="state">The state to log.</param>
        /// <param name="exception">The exception to log, if any.</param>
        /// <param name="formatter">The function to create a log message from the state and exception.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            string message = formatter(state, exception);
            ConsoleLog(logLevel, message);
        }

        /// <summary>
        /// Logs a message to the browser console.
        /// </summary>
        /// <param name="logLevel">The log level of the message.</param>
        /// <param name="message">The message to log.</param>
        private static void ConsoleLog(LogLevel logLevel, string message)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                case LogLevel.Information:
                    Console.WriteLine($"INFO: {message}");
                    break;
                case LogLevel.Warning:
                    Console.WriteLine($"WARN: {message}");
                    break;
                case LogLevel.Error:
                    Console.WriteLine($"ERROR: {message}");
                    break;
                case LogLevel.Critical:
                    Console.WriteLine($"CRITICAL: {message}");
                    break;
                default:
                    Console.WriteLine(message);
                    break;
            }
        }
    }
}