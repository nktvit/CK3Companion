using CompanionDomain.Interfaces.Records;
using CompanionDomain.Utilities;

namespace CompanionDomain.Models.Records;

public class BasicPointsRecord : IPointsRecord
{
    public object Id { get; set; } = URandom.GenerateUniqueId();
    public string CharacterId { get; set; }
    public int Points { get; set; }
}