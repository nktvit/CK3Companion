using NLog;
using NLog.Config;
using NLog.Targets;

namespace CompanionDomain.Utilities;

public static class ULogging
{
    public static Logger ConfigureLogging()
    {
        // Configure NLog
        var config = new LoggingConfiguration();

        var consoleTarget = new ConsoleTarget("console")
        {
            Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message} ${exception}"
        };
        config.AddTarget(consoleTarget);

        config.AddRuleForAllLevels(consoleTarget); // All levels to console

        LogManager.Configuration = config;
        return LogManager.GetCurrentClassLogger();
    }
}