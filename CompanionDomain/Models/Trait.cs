using CompanionDomain.Enums;
using CompanionDomain.Interfaces;
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
    public ICollection<NonApplicableTrait> NonApplicableTraits { get; set; }

    [Ignore]
    public ICollection<SkillModifier> SkillModifiers { get; set; }

    public Trait()
    {
        NonApplicableTraits = new List<NonApplicableTrait>();
        SkillModifiers = new List<SkillModifier>();
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
}
