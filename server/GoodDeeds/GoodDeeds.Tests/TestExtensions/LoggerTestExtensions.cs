using Microsoft.Extensions.Logging;
using Moq;

namespace Gemstone.Loan.API.Tests.TestExtensions;

public static class LoggerTestExtensions
{
    public static void VerifyLogging<T>(this Mock<ILogger<T>> logger, LogLevel logLevel, Times times)
    {
        logger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == logLevel),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), times);
    }
    
    public static void VerifyLogging<T>(this Mock<ILogger<T>> logger, LogLevel logLevel, string message, Times times)
    {
        logger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == logLevel),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => string.Equals(message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), times);
    }
}
