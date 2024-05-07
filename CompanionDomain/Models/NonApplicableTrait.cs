using SQLite;

namespace CompanionDomain.Models;

[Table("NonApplicableTraits")]
public class NonApplicableTrait
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int TraitId { get; set; }

    [Indexed]
    public int OppositeTraitId { get; set; }
}
