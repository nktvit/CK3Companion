using CompanionDomain.Enums;
using CompanionDomain.Models;
using CompanionDomain.Models.Records;

namespace CompanionTests.Trait;

public class TraitModelTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TraitModel_GetSkillModifiers()
    {
        // Arrange
        var trait = new CompanionDomain.Models.Trait
        {
            Id = 1,
            Name = "Test Trait",
            SkillModifiers = new List<SkillModifier>
            {
                new SkillModifier(1, Skill.Diplomacy, 3),
                new SkillModifier(1, Skill.Martial, -2),
                new SkillModifier(1, Skill.Stewardship, 2),
                new SkillModifier(1, Skill.Intrigue, 5),
                new SkillModifier(1, Skill.Learning, 3),
                new SkillModifier(1, Skill.Prowess, 2)
            }
        };
        // Act
        var result = trait.GetSkillModifiers();
        // Assert
        Assert.That(result.Count(), Is.EqualTo(6));
    }
}