using CompanionData;
using CompanionData.ModelsDto;
using CompanionData.Repositories;
using CompanionDomain.Models;

namespace CompanionTests;

public class CharacterRepositoryTests
{
    private string _databasePath;
    private CharacterRepository _characterRepository;

    [SetUp]
    public void Setup()
    {
        var path = "/Users/nick-mba/Library/Containers/com.companyname.companionui/Data/Library/Databases";
        var databaseFolder = Path.Combine(path);
        Directory.CreateDirectory(databaseFolder);
        _databasePath = Path.Combine(databaseFolder, "localStorage.db");

        _characterRepository =
            new CharacterRepository(new DatabaseConnection(_databasePath));

        _characterRepository.DeleteAllCharacters();
    }

    [Test]
    public void CharacterRepository_GetCharacters()
    {
        // Arrange
        _characterRepository.InsertOne(new Character());
        _characterRepository.InsertOne(new Character());
        // Act
        var characters = _characterRepository.GetCharacters();
        // Assert
        Assert.That(characters.Count(), Is.EqualTo(2));
    }

    [Test]
    public void CharacterRepository_GetCharacterById()
    {
        // Arrange
        var character = new Character();
        _characterRepository.InsertOne(character);
        // Act
        var retrievedCharacter = _characterRepository.GetCharacterById(character.Id);
        // Assert
        Assert.That(retrievedCharacter.Id, Is.EqualTo(character.Id));
    }

    [Test]
    public void CharacterRepository_DeleteCharacter()
    {
        // Arrange
        var character = new Character();
        var character1 = new Character();
        _characterRepository.InsertOne(character);
        _characterRepository.InsertOne(character1);
        // Act
        _characterRepository.DeleteCharacter(character);
        // Assert
        Assert.That(_characterRepository.GetCharacters().Count(), Is.EqualTo(1));
    }

    [Test]
    public void CharacterRepository_DeleteCharacterById()
    {
        // Arrange
        var character = new Character();
        _characterRepository.InsertOne(character);
        // Act
        _characterRepository.DeleteCharacterById(character.Id);
        // Assert
        Assert.That(_characterRepository.GetCharacters().Count(), Is.EqualTo(0));
    }

    [Test]
    public void CharacterRepository_DeleteAllCharacters()
    {
        // Arrange
        _characterRepository.InsertOne(new Character());
        _characterRepository.InsertOne(new Character());
        // Act
        _characterRepository.DeleteAllCharacters();
        // Assert
        Assert.That(_characterRepository.GetCharacters().Count(), Is.EqualTo(0));
    }

    [Test]
    public void CharacterRepository_SaveCharacter()
    {
        // Arrange
        var character = new Character();
        // Act
        _characterRepository.SaveCharacter(character);
        // Assert
        Assert.That(_characterRepository.GetCharacters().Count(), Is.EqualTo(1));
    }

    [Test]
    public void CharacterRepository_GetCharacters_Empty()
    {
        // Arrange
        // Act
        var characters = _characterRepository.GetCharacters();
        // Assert
        Assert.That(characters.Count(), Is.EqualTo(0));
    }

    [Test]
    public void CharacterRepository_GetCharacterById_Null()
    {
        // Arrange
        // Act
        var retrievedCharacter = _characterRepository.GetCharacterById("1");
        // Assert
        Assert.That(retrievedCharacter, Is.Null);
    }


    [Test]
    public void CharacterRepository_GetCharacterTraits()
    {
        // Arrange
        TraitRepository traitRepository =
            new TraitRepository(new DatabaseConnection(_databasePath));
        CharacterTraitsRepository characterTraitsRepository =
            new CharacterTraitsRepository(new DatabaseConnection(_databasePath));
        var character = new Character();
        var trait = traitRepository.GetTraitById(34);
        character.AddTrait(trait);
        _characterRepository.SaveCharacter(character);
        characterTraitsRepository.SaveCharacterTrait(new CharacterTrait
            { CharacterId = character.Id, TraitId = trait.Id });
        // Act
        var characterTraits = characterTraitsRepository.GetCharacterTraitsIds(character);
        // Assert
        Assert.That(characterTraits.Count(), Is.EqualTo(1));
    }
}