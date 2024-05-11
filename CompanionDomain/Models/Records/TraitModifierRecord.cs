using CompanionDomain.Interfaces.Records;

namespace CompanionDomain.Models.Records;

public class TraitModifierRecord : IModifierRecord
{
    public int Id { get; set; }
    public int TraitId { get; set; }
    public int Modifier { get; set; }
}