using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IntraDotNet.AspNetCore.Wasm.BrowserConsole
{
    /// <summary>
    /// Extension methods for setting up browser console logging services in an <see cref="ILoggingBuilder" />.
    /// </summary>
    public static class BrowserConsoleDependencyInjectionExtensions
    {
        /// <summary>
        /// Adds a browser console logger to the logging builder.
        /// </summary>
        /// <param name="loggingBuilder">The logging builder to add the browser console logger to.</param>
        /// <returns>The logging builder with the browser console logger added.</returns>
        public static ILoggingBuilder AddBrowserConsole(this ILoggingBuilder loggingBuilder)
        {
            // Get LogLevel from configuration
            IConfiguration? configuration = loggingBuilder.Services.BuildServiceProvider().GetService<IConfiguration>();

            if (configuration != null)
            {
                loggingBuilder.Services.AddSingleton<ILoggerProvider, BrowserConsoleLoggerProvider>(services =>
                {
                    return new BrowserConsoleLoggerProvider(configuration);
                });

                loggingBuilder.Services.AddSingleton<ILogger, BrowserConsoleLogger>(provider => new BrowserConsoleLogger(configuration.GetValue<LogLevel>("Logging:LogLevel:Default")));
            }

            return loggingBuilder;
        }
    }
}