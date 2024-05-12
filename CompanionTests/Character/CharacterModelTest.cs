using CompanionData;
using CompanionData.Repositories;
using CompanionDomain.Models;
using CompanionDomain.Utilities;
using NLog;

namespace CompanionTests;

public class CharacterModelTest
{
    private string _databasePath;
    private TraitRepository _traitRepository;
    private Logger _logger;
    
    [SetUp]
    public void Setup()
    {
        var path = "/Users/nick-mba/Library/Containers/com.companyname.companionui/Data/Library/Databases";
        var databaseFolder = Path.Combine(path);
        Directory.CreateDirectory(databaseFolder);
        _databasePath = Path.Combine(databaseFolder, "localStorage.db");
        
        _traitRepository =
            new TraitRepository(new DatabaseConnection(_databasePath, _logger), _logger);

        CharacterRepository characterRepository =
            new CharacterRepository(new DatabaseConnection(_databasePath, _logger), _logger);

        characterRepository.DeleteAllCharacters();

        _logger = ULogging.ConfigureLogging();

    }
    
    [Test]
    public void CharacterModel_AddTrait()
    {
        // Arrange
        var character = new Character();
        var trait1 = _traitRepository.GetTraitById(34);
        var trait2 = _traitRepository.GetTraitById(33);
        // Act
        character.AddTrait(trait1);
        character.AddTrait(trait2);
        // Assert
        Assert.That(character.Traits.Contains(trait1), Is.EqualTo(true));
        Assert.That(character.Traits.Contains(trait2), Is.EqualTo(true));
    }
    [Test]
    public void CharacterModel_RemoveTrait()
    {
        // Arrange
        var character = new Character();
        var trait1 = _traitRepository.GetTraitById(34);
        var trait2 = _traitRepository.GetTraitById(33);
        character.AddTrait(trait1);
        character.AddTrait(trait2);

        // Act
        character.RemoveTrait(trait1);
        // Assert
        Assert.That(character.Traits.Contains(trait1), Is.EqualTo(false));
        Assert.That(character.Traits.Contains(trait2), Is.EqualTo(true));
    }
    [Test]
    public void CharacterModel_AddDuplicateTrait()
    {
        // Arrange

        var character = new Character();
        var trait1 = _traitRepository.GetTraitById(34);
        var trait2 = _traitRepository.GetTraitById(33);
        character.AddTrait(trait1);
        character.AddTrait(trait2);

        // Act
        // Assert
        //Assert that the method throws an exception
        Assert.That(() => character.AddTrait(trait1), Throws.Exception);
        Assert.That(character.Traits.Contains(trait1), Is.EqualTo(true));
        Assert.That(character.Traits.Contains(trait2), Is.EqualTo(true));
    }

    [Test]
    public void CharacterModel_AddDublicateTraits()
    {
        // Arrange
        var character = new Character();
        var trait1 = _traitRepository.GetTraitById(34);
        var trait2 = _traitRepository.GetTraitById(33);
        character.AddTrait(trait1);
        character.AddTrait(trait2);

        // Act
        // Assert
        //Assert that the method throws an exception
        Assert.That(() => character.AddTraits(new List<CompanionDomain.Models.Trait> {trait1, trait2}), Throws.Exception);
        Assert.That(character.Traits.Contains(trait1), Is.EqualTo(true));
        Assert.That(character.Traits.Contains(trait2), Is.EqualTo(true));
    }
}