using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace IntraDotNet.AspNetCore.Wasm.BrowserConsole
{
    /// <summary>
    /// A provider for creating instances of <see cref="BrowserConsoleLogger"/>.
    /// </summary>
    public class BrowserConsoleLoggerProvider : ILoggerProvider
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserConsoleLoggerProvider"/> class.
        /// </summary>
        /// <param name="configuration">The configuration to use for logger settings.</param>
        public BrowserConsoleLoggerProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Creates a new instance of <see cref="BrowserConsoleLogger"/>.
        /// </summary>
        /// <param name="categoryName">The category name for the logger.</param>
        /// <returns>A new instance of <see cref="BrowserConsoleLogger"/>.</returns>
        public ILogger CreateLogger(string categoryName)
        {
            // Use the configuration as needed
            LogLevel logLevel = _configuration.GetValue<LogLevel>("Logging:LogLevel:Default");
            
            return new BrowserConsoleLogger(logLevel);
        }

        /// <summary>
        /// Disposes resources used by the provider.
        /// </summary>
        public void Dispose()
        {
        }
    }
}