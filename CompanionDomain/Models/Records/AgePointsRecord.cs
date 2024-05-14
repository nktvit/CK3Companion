using CompanionDomain.Interfaces.Records;
using CompanionDomain.Utilities;

namespace CompanionDomain.Models.Records;

//computed model
public class AgePointsRecord : IPointsRecord
{
    public object Id { get; set; } = URandom.GenerateUniqueId();
    public int Points { get; set; }
    public int Age { get; set; }
}