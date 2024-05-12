using CompanionData;
using CompanionData.Repositories;
using CompanionData.Services;
using CompanionDomain.Models;
using NLog;

namespace CompanionTests;

public class CharacterServiceTest
{
    private CharacterRepository _characterRepository;
    private CharacterTraitsRepository _characterTraitsRepository;
    private TraitRepository _traitRepository;
    private Logger _logger;
    private string _databasePath;

    [SetUp]
    public void Setup()
    {
        var path = "/Users/nick-mba/Library/Containers/com.companyname.companionui/Data/Library/Databases";
        var databaseFolder = Path.Combine(path);
        Directory.CreateDirectory(databaseFolder);
        _databasePath = Path.Combine(databaseFolder, "localStorage.db");

        _characterRepository =
            new CharacterRepository(new DatabaseConnection(_databasePath));
        _characterTraitsRepository =
            new CharacterTraitsRepository(new DatabaseConnection(_databasePath));
        _traitRepository =
            new TraitRepository(new DatabaseConnection(_databasePath));

        _characterRepository.DeleteAllCharacters();
    }

    [Test]
    public void CharacterService_GetCharacters()
    {
        // Arrange
        var characterService = new CharacterService(_characterRepository, _characterTraitsRepository, _traitRepository, _logger);
        characterService.SaveCharacter(new Character());
        characterService.SaveCharacter(new Character());
        // Act
        var characters = characterService.GetAllCharacters();
        // Assert
        Assert.That(characters.Count(), Is.EqualTo(2));
    }
}