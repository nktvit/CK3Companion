using CompanionDomain.Interfaces.Records;

namespace CompanionDomain.Models.Records;

//computed model
public class AgePointsRecord : IPointsRecord {
    public int Id { get; set; }
    public int Points { get; set; }
    public int Age { get; set; }
}