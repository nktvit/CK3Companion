using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanionDomain.Enums;
using CompanionDomain.Interfaces;

namespace CompanionDomain.Models;

public class SkillModifier 
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("TraitId")]

    public int TraitId { get; set; }

    [Required]
    public Skill Skill { get; set; }

    [Required]
    public int Modifier { get; set; }

    public Trait Trait { get; set; }
}