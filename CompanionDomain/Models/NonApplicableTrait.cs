using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanionDomain.Models;

public class NonApplicableTrait
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TraitId { get; set; }

    [Required]
    public int OppositeTraitId { get; set; }

    // Navigation Properties for Relationships with Trait
    [ForeignKey("TraitId")]
    public virtual Trait Trait { get; set; }

    [ForeignKey("OppositeTraitId")]
    public virtual Trait OppositeTrait { get; set; } 
}