using CompanionData;
using CompanionData.Repositories;
using CompanionDomain.Models;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace CompanionTests;

public class CharacterTests
{
    private string databasePath;
    private Logger logger;

    [SetUp]
    public void Setup()
    {
        var path = "/Users/nick-mba/Library/Containers/com.companyname.companionui/Data/Library/Databases";
        var databaseFolder = Path.Combine(path);
        Directory.CreateDirectory(databaseFolder);
        databasePath = Path.Combine(databaseFolder, "localStorage.db");

        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(databasePath, logger), logger);

        characterRepository.DeleteAllCharacters();

        logger = ConfigureLogging();

        static Logger ConfigureLogging()
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

    [Test]
    public void CharacterRepository_GetCharacters()
    {
        // Arrange
        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(databasePath, logger), logger);
        characterRepository.InsertOne(new Character());
        characterRepository.InsertOne(new Character());
        // Act
        var characters = characterRepository.GetCharacters();
        // Assert
        Assert.That(characters.Count(), Is.EqualTo(2));
    }
    [Test]
    public void CharacterRepository_GetCharacterById()
    {
        // Arrange
        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(databasePath, logger), logger);
        var character = new Character();
        characterRepository.InsertOne(character);
        // Act
        var retrievedCharacter = characterRepository.GetCharacterById(character.Id);
        // Assert
        Assert.That(retrievedCharacter.Id, Is.EqualTo(character.Id));
    }
    [Test]
    public void CharacterRepository_DeleteCharacter()
    {
        // Arrange
        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(databasePath, logger), logger);
        var character = new Character();
        characterRepository.InsertOne(character);
        // Act
        characterRepository.DeleteCharacter(character);
        // Assert
        Assert.That(characterRepository.GetCharacters().Count(), Is.EqualTo(0));
    }
    [Test]
    public void CharacterRepository_DeleteCharacterById()
    {
        // Arrange
        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(databasePath, logger), logger);
        var character = new Character();
        characterRepository.InsertOne(character);
        // Act
        characterRepository.DeleteCharacterById(character.Id);
        // Assert
        Assert.That(characterRepository.GetCharacters().Count(), Is.EqualTo(0));
    }
    [Test]
    public void CharacterRepository_DeleteAllCharacters()
    {
        // Arrange
        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(databasePath, logger), logger);
        characterRepository.InsertOne(new Character());
        characterRepository.InsertOne(new Character());
        // Act
        characterRepository.DeleteAllCharacters();
        // Assert
        Assert.That(characterRepository.GetCharacters().Count(), Is.EqualTo(0));
    }
    [Test]
    public void CharacterRepository_SaveCharacter()
    {
        // Arrange
        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(databasePath, logger), logger);
        var character = new Character();
        // Act
        characterRepository.SaveCharacter(character);
        // Assert
        Assert.That(characterRepository.GetCharacters().Count(), Is.EqualTo(1));
    }
    [Test]
    public void CharacterRepository_GetCharacters_Empty()
    {
        // Arrange
        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(databasePath, logger), logger);
        // Act
        var characters = characterRepository.GetCharacters();
        // Assert
        Assert.That(characters.Count(), Is.EqualTo(0));
    }
    [Test]
    public void CharacterRepository_GetCharacterById_Null()
    {
        // Arrange
        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(databasePath, logger), logger);
        // Act
        var retrievedCharacter = characterRepository.GetCharacterById("1");
        // Assert
        Assert.That(retrievedCharacter, Is.Null);
    }
    // [Test]
    // public void Character
}