using CompanionDomain.Interfaces.Records;

namespace CompanionDomain.Models.Records;

public class BasicPointsRecord : IPointsRecord
{
    public int Id { get; set; }
    public string CharacterId { get; set; }
    public int Points { get; set; }
}