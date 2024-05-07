using System.Reflection;
using CompanionData;
using CompanionData.Repositories;
using NLog;
using NLog.Config;
using NLog.Targets;


namespace CompanionUI;

public class MauiProgram
{
    private static Logger _logger = ConfigureLogging();
    public static async Task<MauiApp> CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif


        // Request file system permissions
        var filePermission = await Permissions.RequestAsync<Permissions.StorageRead>();
        if (filePermission != PermissionStatus.Granted)
        {
            _logger.Warn("File system permissions denied.");
            // Handle the permission denial or show an error message
            return builder.Build();
        }

        // Register the DatabaseRepository service
        RegisterDatabaseServices(builder.Services);

        return builder.Build();
    }

    private static Logger ConfigureLogging()
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
    private static void RegisterDatabaseServices(IServiceCollection services)
    {
        services.AddSingleton<DatabaseConnection>(provider =>
        {
            var databaseFolder = Path.Combine(FileSystem.AppDataDirectory, "Databases");
            Directory.CreateDirectory(databaseFolder);
            var databasePath = Path.Combine(databaseFolder, "localStorage.db");
            _logger.Debug("Database path: {0}", databasePath);

#if DEBUG
            _logger.Info("Debug mode enabled. Deleting existing database file if it exists.");
            // Force overwrite in debug mode
            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }
#endif

            // Check if the database file exists in the application's data directory
            if (!File.Exists(databasePath))
            {
                try
                {
                    // If the file doesn't exist, copy it from the embedded resource
                    using var stream = Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("CompanionUI.Resources.localStorage.db");
                    _logger.Info("Copying database file from embedded resources.");

                    _logger.Debug("stream is null: {0}", stream == null);

                    using var fileStream = File.Create(databasePath);
                    stream.CopyTo(fileStream);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error copying database file from embedded resources.");
                }
                
            }

            return new DatabaseConnection(databasePath, _logger);
        });

        services.AddSingleton<TraitRepository>(provider =>
        {
            var databaseConnection = provider.GetRequiredService<DatabaseConnection>();
            return new TraitRepository(databaseConnection, _logger);
        });
    }
}