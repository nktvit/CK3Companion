using CompanionDomain.Enums;
using CompanionDomain.Interfaces.Records;
using CompanionDomain.Utilities;

namespace CompanionDomain.Models.Records;

public class TraitModifierRecord : IModifierRecord
{
    public object Id { get; set; } = URandom.GenerateUniqueId();
    public int TraitId { get; set; }
    public int Modifier { get; set; }
    public Skill Skill { get; set; }

    public TraitModifierRecord(int traitId, int modifier, Skill skill)
    {
        TraitId = traitId;
        Modifier = modifier;
        Skill = skill;
    }
}