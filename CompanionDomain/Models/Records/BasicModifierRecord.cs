using CompanionDomain.Enums;
using CompanionDomain.Interfaces.Records;
using CompanionDomain.Utilities;

namespace CompanionDomain.Models.Records;

public class BasicModifierRecord : IModifierRecord
{
    public object Id { get; set; } = URandom.GenerateUniqueId();
    public int Modifier { get; set; }
    public Skill Skill { get; set; }
}