using CompanionDomain.Interfaces.Records;

namespace CompanionDomain.Models.Records;

public class BasicModifierRecord : IModifierRecord
{
    public int Id { get; set; }
    public int Modifier { get; set; }
}