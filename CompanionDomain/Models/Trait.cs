using CompanionDomain.Enums;
using CompanionDomain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanionDomain.Models;

public class Trait : ITrait
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Trait name cannot exceed 100 characters.")]
    public string Name { get; set; }
    [Required]
    public TraitType Type { get; set; }
    [Column("designer_cost")]
    public int DesignerCost { get; set; } // positive, negative, or 0
    [Column("minimum_age")]
    public int MinimumAge { get; set; }
    [Column("maximum_age")]
    public int MaximumAge { get; set; }
    
    public ICollection<NonApplicableTrait> NonApplicableTraits { get; set; }
    public ICollection<SkillModifier> SkillModifiers { get; set; }
    
    public Trait(int id, string name, TraitType type, int designerCost = 0, int minimumAge = 0, int maximumAge = 0)
    {
        Id = id;
        Name = name;
        Type = type;
        DesignerCost = designerCost;
        MinimumAge = minimumAge;
        MaximumAge = maximumAge;
    }
}