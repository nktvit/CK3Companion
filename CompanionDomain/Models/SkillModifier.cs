using System.ComponentModel.DataAnnotations;
using CompanionDomain.Enums;
using SQLite;

namespace CompanionDomain.Models;

[Table("SkillModifiers")]
public class SkillModifier
{
    [Indexed]
    public int TraitId { get; set; }

    public Skill Skill { get; set; }

    public int Modifier { get; set; }
}