using CompanionData;
using CompanionData.ModelsDto;
using CompanionData.Repositories;
using CompanionDomain.Models;
using CompanionDomain.Utilities;
using NLog;


namespace CompanionTests;

public class CharacterRepositoryTests
{
    private string _databasePath;
    private Logger _logger;

    [SetUp]
    public void Setup()
    {
        var path = "/Users/nick-mba/Library/Containers/com.companyname.companionui/Data/Library/Databases";
        var databaseFolder = Path.Combine(path);
        Directory.CreateDirectory(databaseFolder);
        _databasePath = Path.Combine(databaseFolder, "localStorage.db");

        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(_databasePath, _logger), _logger);

        characterRepository.DeleteAllCharacters();

        _logger = ULogging.ConfigureLogging();
    }

    [Test]
    public void CharacterRepository_GetCharacters()
    {
        // Arrange
        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(_databasePath, _logger), _logger);
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
            new CharacterRepository(new DatabaseConnection(_databasePath, _logger), _logger);
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
            new CharacterRepository(new DatabaseConnection(_databasePath, _logger), _logger);
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
            new CharacterRepository(new DatabaseConnection(_databasePath, _logger), _logger);
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
            new CharacterRepository(new DatabaseConnection(_databasePath, _logger), _logger);
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
            new CharacterRepository(new DatabaseConnection(_databasePath, _logger), _logger);
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
            new CharacterRepository(new DatabaseConnection(_databasePath, _logger), _logger);
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
            new CharacterRepository(new DatabaseConnection(_databasePath, _logger), _logger);
        // Act
        var retrievedCharacter = characterRepository.GetCharacterById("1");
        // Assert
        Assert.That(retrievedCharacter, Is.Null);
    }


    [Test]
    public void CharacterRepository_GetCharacterTraits()
    {
        // Arrange
        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(_databasePath, _logger), _logger);
        TraitRepository traitRepository =
            new TraitRepository(new DatabaseConnection(_databasePath, _logger), _logger);
        CharacterTraitsRepository characterTraitsRepository =
            new CharacterTraitsRepository(new DatabaseConnection(_databasePath, _logger), _logger);
        var character = new Character();
        var trait = traitRepository.GetTraitById(34);
        character.AddTrait(trait);
        characterRepository.SaveCharacter(character);
        characterTraitsRepository.SaveCharacterTrait(new CharacterTrait
            { CharacterId = character.Id, TraitId = trait.Id });
        // Act
        var characterTraits = characterTraitsRepository.GetCharacterTraitsIds(character);
        // Assert
        Assert.That(characterTraits.Count(), Is.EqualTo(1));
    }
}