using CompanionDomain.Models;
using Microsoft.Extensions.Logging;

namespace CompanionData.Repositories;

public class SkillModifiersRepository : DataRepository<SkillModifier>
{
    public SkillModifiersRepository(DatabaseConnection databaseConnection, ILogger<SkillModifiersRepository> logger)
        : base(databaseConnection, logger)
    {
    }
    
    public IEnumerable<SkillModifier> GetTraitSkillModifiers(Trait trait)
    {
        return GetFiltered(x => x.TraitId == trait.Id);
    }
}