using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CompanionData;
using CompanionData.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace CompanionUI;

public class MauiProgram
{
    private static ILogger<MauiProgram> Logger { get; set; }

    public static async Task<MauiApp> CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        // Configure logging
        ConfigureLogging(builder);
#endif


        // Request file system permissions
        var filePermission = await Permissions.RequestAsync<Permissions.StorageRead>();
        if (filePermission != PermissionStatus.Granted)
        {
            Logger.LogError("File system permissions denied.");
            // Handle the permission denial or show an error message
            return builder.Build();
        }

        // Register the DatabaseRepository service
        RegisterDatabaseServices(builder.Services, Logger);

        return builder.Build();
    }

    private static void ConfigureLogging(MauiAppBuilder builder)
    {
        Logger = builder.Services.AddLogging(configure =>
            {
                configure.AddDebug();
                configure.AddConsole();
            })
            .BuildServiceProvider()
            .GetRequiredService<ILogger<MauiProgram>>();
    }

    private static void RegisterDatabaseServices(IServiceCollection services, ILogger logger)
    {
        services.AddSingleton<DatabaseConnection>(provider =>
        {
            var databaseFolder = Path.Combine(FileSystem.AppDataDirectory, "Databases");
            Directory.CreateDirectory(databaseFolder);
            var databasePath = Path.Combine(databaseFolder, "localStorage.db");

#if DEBUG
            // Force overwrite in debug mode
            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }
#endif

            // Check if the database file exists in the application's data directory
            if (!File.Exists(databasePath))
            {
                // If the file doesn't exist, copy it from the embedded resource
                using var stream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("CompanionUI.Resources.localStorage.db");

                using var fileStream = File.Create(databasePath);
                stream.CopyTo(fileStream);
            }

            var logger = provider.GetRequiredService<ILogger<DatabaseConnection>>();
            return new DatabaseConnection(databasePath, logger);
        });

        services.AddSingleton<TraitRepository>(provider =>
        {
            var databaseConnection = provider.GetRequiredService<DatabaseConnection>();
            var logger = provider.GetRequiredService<ILogger<TraitRepository>>();
            return new TraitRepository(databaseConnection, logger);
        });
    }
}