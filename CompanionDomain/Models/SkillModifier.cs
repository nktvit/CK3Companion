using CompanionDomain.Enums;
using SQLite;

namespace CompanionDomain.Models;

[Table("SkillModifiers")]
public class SkillModifier
{
    public SkillModifier()
    {
        
    }
    public SkillModifier(int traitId, Skill skill, int modifier)
    {
        TraitId = traitId;
        Skill = skill;
        Modifier = modifier;
    }

    [Indexed]
    public int TraitId { get; set; }

    // Internal store for database representation
    [Column("Skill")] // Ensure this maps to the correct column
    private string SkillString { get; set; }

    // Public property with conversion logic
    public Skill Skill 
    { 
        get => StringToSkillEnum(SkillString);
        set => SkillString = SkillEnumToString(value);
    }

    public int Modifier { get; set; }
    
    
    private Skill StringToSkillEnum(string skillString)
    {
        if (Enum.TryParse(skillString, out Skill skill))
        {
            return skill;
        }
        else
        {
            throw new Exception($"Invalid skill string encountered: {skillString}");
        }
    }

    private string SkillEnumToString(Skill skill)
    {
        return skill.ToString();
    }
}