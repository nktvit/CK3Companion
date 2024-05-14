using CompanionData;
using CompanionData.Repositories;
using CompanionDomain.Enums;
using CompanionDomain.Models;
using Moq;

namespace CompanionTests;

public class CharacterModelTest
{
    private string _databasePath;
    private TraitRepository _traitRepository;
    private Mock<TraitRepository> _mockTraitRepository;
    private Mock<SkillModifierRepository> _mockSkillModifierRepository;
    private Mock<NonApplicableTraitRepository> _mockNonApplicableTraitRepository;

    [SetUp]
    public void Setup()
    {
        var path = "/Users/nick-mba/Library/Containers/com.companyname.companionui/Data/Library/Databases";
        var databaseFolder = Path.Combine(path);
        Directory.CreateDirectory(databaseFolder);
        _databasePath = Path.Combine(databaseFolder, "localStorage.db");
        
        _traitRepository = new TraitRepository(new DatabaseConnection(_databasePath));

        _mockTraitRepository = new Mock<TraitRepository>();
        _mockSkillModifierRepository = new Mock<SkillModifierRepository>();
        _mockNonApplicableTraitRepository = new Mock<NonApplicableTraitRepository>();
    }

    [Test]
    public void CharacterModel_AddTraitAndApplySkillModifiers()
    {
        // Arrange
        var character = new Character();
        var trait = new CompanionDomain.Models.Trait { Id = 1, Name = "Trait 1" };
        trait.SkillModifiers = new List<SkillModifier>
        {
            new SkillModifier { TraitId = 1, Skill = Skill.Diplomacy, Modifier = 2 },
            new SkillModifier { TraitId = 1, Skill = Skill.Intrigue, Modifier = -1 }
        };

        // Act
        character.AddTrait(trait);

        // Assert
        Assert.That(character.Traits.Contains(trait), Is.True);
        Assert.That(character.GetSkillValue(Skill.Diplomacy), Is.EqualTo(2));  // Check individual skill value
        Assert.That(character.GetSkillValue(Skill.Intrigue), Is.EqualTo(-1)); // Check individual skill value
    }

    [Test]
    public void CharacterModel_AddTraitsAndApplySkillModifiers()
    {
        // Arrange
        var character = new Character();
        var traits = new List<CompanionDomain.Models.Trait>
        {
            new CompanionDomain.Models.Trait { Id = 1, Name = "Trait 1" },
            new CompanionDomain.Models.Trait { Id = 2, Name = "Trait 2" }
        };

        traits[0].SkillModifiers = new List<SkillModifier>
        {
            new SkillModifier { TraitId = 1, Skill = Skill.Diplomacy, Modifier = 2 },
            new SkillModifier { TraitId = 1, Skill = Skill.Intrigue, Modifier = -1 }
        };
        traits[1].SkillModifiers = new List<SkillModifier>
        {
            new SkillModifier { TraitId = 2, Skill = Skill.Diplomacy, Modifier = 1 },
            new SkillModifier { TraitId = 2, Skill = Skill.Intrigue, Modifier = 1 }
        };

        // Act
        character.AddTraits(traits);

        // Assert
        Assert.That(character.Traits.Contains(traits[0]), Is.True);
        Assert.That(character.Traits.Contains(traits[1]), Is.True);
        Assert.That(character.GetSkillValue(Skill.Diplomacy), Is.EqualTo(3));  // Check summed skill value
        Assert.That(character.GetSkillValue(Skill.Intrigue), Is.EqualTo(0));    // Check summed skill value
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
        Assert.That(() => character.AddTraits(new List<CompanionDomain.Models.Trait> { trait1, trait2 }),
            Throws.Exception);
        Assert.That(character.Traits.Contains(trait1), Is.EqualTo(true));
        Assert.That(character.Traits.Contains(trait2), Is.EqualTo(true));
    }
}