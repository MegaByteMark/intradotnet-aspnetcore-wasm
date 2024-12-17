using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace IntraDotNet.AspNetCore.Wasm.BrowserConsole.Tests
{
    public class BrowserConsoleLoggerProviderTests
    {
        [Fact]
        public void CreateLogger_ShouldReturnLoggerInstance()
        {
            // Arrange
            ServiceCollection services = new ServiceCollection();
            var inMemorySettings = new Dictionary<string, string?> {
                {"Logging:LogLevel:Default",  "Information"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            //Add logging configuration to services
            services.AddSingleton(configuration);

            ILoggingBuilder loggingBuilder = Substitute.For<ILoggingBuilder>();
            loggingBuilder.Services.Returns(services);

            // Act
            loggingBuilder.AddBrowserConsole();

            ServiceProvider provider = services.BuildServiceProvider();
            ILoggerProvider? loggerProvider = provider.GetService<ILoggerProvider>();

            // Act
            ILogger logger = loggerProvider!.CreateLogger("TestCategory");

            // Assert
            Assert.NotNull(logger);
            Assert.IsType<BrowserConsoleLogger>(logger);
        }
    }
}