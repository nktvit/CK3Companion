using CompanionDomain.Enums;
using CompanionDomain.Utilities;
using SQLite;

namespace CompanionDomain.Models;

[Table("Dynasties")]
public class Dynasty
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = "Unnamed Dynasty";

    [MaxLength(500)]
    [Column("description")]
    public string Description { get; set; } = "No description available.";

    [Column("level")]
    public SplendorLevel Level { get; set; } = SplendorLevel.BaseOrigins;

    [Column("head_id")]
    public int HeadId { get; set; }

    [Ignore]
    public Character Head { get; set; }

    public Dynasty()
    {
        // No need to generate Id manually since it's set to AutoIncrement
    }
}