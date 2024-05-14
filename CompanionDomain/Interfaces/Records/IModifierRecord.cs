namespace CompanionDomain.Interfaces.Records;

public interface IModifierRecord : IRecord
{
    public new object Id { get; set; }
    public int Modifier { get; set; }
}