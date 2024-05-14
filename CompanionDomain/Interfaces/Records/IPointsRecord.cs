namespace CompanionDomain.Interfaces.Records;

public interface IPointsRecord : IRecord
{
    public new object Id { get; set; }
    // Customization points
    public int Points { get; set; }
}