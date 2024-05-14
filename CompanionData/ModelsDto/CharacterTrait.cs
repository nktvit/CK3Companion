using SQLite;

namespace CompanionData.ModelsDto;

[Table("CharacterTraits")]
public class CharacterTrait
{
    [Column("CharacterId")]
    public string CharacterId { get; set; }
    [Column("TraitId")]
    public int TraitId { get; set; }
}