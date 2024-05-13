using CompanionData;
using CompanionData.Repositories;
using CompanionData.Services;
using CompanionDomain.Models;
using CompanionDomain.Utilities;
using NLog;

namespace CompanionTests;

public class CharacterServiceTest
{
    private CharacterRepository _characterRepository;
    private CharacterTraitsRepository _characterTraitsRepository;
    private TraitRepository _traitRepository;
    private CharacterService _characterService;
    private Logger _logger;
    private string _databasePath;

    [SetUp]
    public void Setup()
    {
        var path = "/Users/nick-mba/Library/Containers/com.companyname.companionui/Data/Library/Databases";
        var databaseFolder = Path.Combine(path);
        Directory.CreateDirectory(databaseFolder);
        _databasePath = Path.Combine(databaseFolder, "localStorage.db");

        _logger = ULogging.ConfigureLogging();
        _characterRepository =
            new CharacterRepository(new DatabaseConnection(_databasePath));
        _characterTraitsRepository =
            new CharacterTraitsRepository(new DatabaseConnection(_databasePath));
        _traitRepository =
            new TraitRepository(new DatabaseConnection(_databasePath));
        _characterService =
            new CharacterService(_characterRepository, _characterTraitsRepository, _traitRepository, _logger);

        _characterRepository.DeleteAllCharacters();
    }

    [Test]
    public void CharacterService_GetCharacters()
    {
        // Arrange
        _characterService.SaveCharacter(new Character());
        _characterService.SaveCharacter(new Character());
        // Act
        var characters = _characterService.GetAllCharacters();
        // Assert
        Assert.That (characters.Count(), Is.EqualTo(2));
    }
    // [Test]
    // public void CharacterService_ApplyTrait()
}