using CompanionDomain.Enums;
using CompanionDomain.Interfaces;
using CompanionDomain.Models.Records;
using SQLite;

namespace CompanionDomain.Models;

[Table("Traits")]
public class Trait : ITrait
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; }
    [Column("type")]
    public TraitType Type { get; set; }

    [Column("designer_cost")]
    public int DesignerCost { get; set; }

    [Column("minimum_age")]
    public int MinimumAge { get; set; }

    [Column("maximum_age")]
    public int MaximumAge { get; set; }

    [Ignore]
    public IEnumerable<NonApplicableTrait> NonApplicableTraits { get; set; } = new List<NonApplicableTrait>();

    [Ignore]
    public IEnumerable<SkillModifier> SkillModifiers { get; set; } = new List<SkillModifier>();

    public Trait()
    {
    }

    public Trait(int id, string name, TraitType type, int designerCost = 0, int minimumAge = 0, int maximumAge = 0)
    {
        Id = id;
        Name = name;
        Type = type;
        DesignerCost = designerCost;
        MinimumAge = minimumAge;
        MaximumAge = maximumAge;
        NonApplicableTraits = new List<NonApplicableTrait>();
        SkillModifiers = new List<SkillModifier>();
    }
    
    public IEnumerable<TraitModifierRecord> GetTraitModifiersRecords()
    {
        return SkillModifiers.Select(sm => new TraitModifierRecord(sm.TraitId, sm.Modifier, sm.Skill ));
    }
    public IEnumerable<SkillModifier> GetSkillModifiers()
    {
        return SkillModifiers;
    }
}
