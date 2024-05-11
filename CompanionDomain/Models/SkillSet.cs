using SQLite;

namespace CompanionDomain.Models;

public class SkillSet
{
    [PrimaryKey]
    [Column("id")]
    public string Id { get; set; }
    [Column("character_id")]
    public string CharacterId { get; set; }

    // public Dictionary<> Type { get; set; }
}