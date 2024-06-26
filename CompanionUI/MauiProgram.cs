﻿using System;
using System.Reflection;
using System.Globalization;
using CompanionData;
using CompanionData.Repositories;
using CompanionData.Services;
using CompanionDomain.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using NLog;

namespace CompanionUI;

public class MauiProgram
{
    private static readonly Logger Logger = ULogging.ConfigureLogging();

    public static async Task<MauiApp> CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            //todo: add fonts
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        // Configure localization services
        builder.Services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources/Localization"; // Specify the path where resource files are located
        });
        var supportedCultures = new[] { "en-US", "fr-FR" };
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("en-US");
            options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
            options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
        });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddBlazorWebViewDeveloperTools();


        // Request file system permissions
        var filePermission = await Permissions.RequestAsync<Permissions.StorageRead>();
        if (filePermission != PermissionStatus.Granted)
        {
            Logger.Warn("File system permissions denied.");
            // Handle the permission denial or show an error message
            return builder.Build();
        }

        // Register the DatabaseRepository service
        RegisterDatabaseServices(builder.Services);

        return builder.Build();
    }

    private static void RegisterDatabaseServices(IServiceCollection services)
    {
        services.AddSingleton<DatabaseConnection>(provider =>
        {
            var databaseFolder = Path.Combine(FileSystem.AppDataDirectory, "Databases");
            Directory.CreateDirectory(databaseFolder);
            var databasePath = Path.Combine(databaseFolder, "localStorage.db");
            Logger.Debug("Database path: {0}", databasePath);

            //     Logger.Info("Deleting existing database file if it exists.");
            //     if (File.Exists(databasePath))
            //     {
            //         File.Delete(databasePath);
            //     }

            // Check if the database file exists in the application's data directory
            if (!File.Exists(databasePath))
            {
                try
                {
                    // If the file doesn't exist, copy it from the embedded resource
                    using var stream = Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("CompanionUI.Resources.localStorage.db");
                    Logger.Info("Copying database file from embedded resources.");

                    Logger.Debug("Assembly Name: {0}", Assembly.GetExecutingAssembly().FullName);
                    Logger.Debug("Resource Names:");

                    foreach (var name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                    {
                        Logger.Debug("  Resource: {0}", name);
                    }

                    Logger.Debug("stream is null: {0}", stream == null); // Check if null


                    using var fileStream = File.Create(databasePath);
                    stream.CopyTo(fileStream);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Error copying database file from embedded resources.");
                }
            }

            return new DatabaseConnection(databasePath);
        });

        // Repositories registered as singletons
        services.AddSingleton<TraitRepository>(provider =>
        {
            var databaseConnection = provider.GetRequiredService<DatabaseConnection>();
            return new TraitRepository(databaseConnection);
        });
        services.AddSingleton<SkillModifierRepository>(provider =>
        {
            var databaseConnection = provider.GetRequiredService<DatabaseConnection>();
            return new SkillModifierRepository(databaseConnection);
        });
        services.AddSingleton<NonApplicableTraitRepository>(provider =>
        {
            var databaseConnection = provider.GetRequiredService<DatabaseConnection>();
            return new NonApplicableTraitRepository(databaseConnection);
        });
        services.AddSingleton<CharacterRepository>(provider =>
        {
            var databaseConnection = provider.GetRequiredService<DatabaseConnection>();
            return new CharacterRepository(databaseConnection);
        });
        services.AddSingleton<CharacterTraitsRepository>(provider =>
        {
            var databaseConnection = provider.GetRequiredService<DatabaseConnection>();
            return new CharacterTraitsRepository(databaseConnection);
        });
        // Services registered as singletons
        services.AddSingleton<TraitService>(provider =>
        {
            var traitRepository = provider.GetRequiredService<TraitRepository>();
            var skillModifierRepository = provider.GetRequiredService<SkillModifierRepository>();
            var nonApplicableTraitRepository = provider.GetRequiredService<NonApplicableTraitRepository>();
            return new TraitService(traitRepository, skillModifierRepository, nonApplicableTraitRepository, Logger);
        });
        services.AddSingleton<CharacterService>(provider =>
        {
            var characterRepository = provider.GetRequiredService<CharacterRepository>();
            var characterTraitsRepository = provider.GetRequiredService<CharacterTraitsRepository>();
            var traitRepository = provider.GetRequiredService<TraitRepository>();
            var traitService = provider.GetRequiredService<TraitService>();
            return new CharacterService(characterRepository, characterTraitsRepository, traitRepository, traitService, Logger);
        });
        services.AddScoped<IStringLocalizer, StringLocalizer<App>>();
    }
}