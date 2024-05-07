using CompanionDomain.Models;
using NLog;

namespace CompanionData.Repositories;

public class SkillModifiersRepository : DataRepository<SkillModifier>
{
    public SkillModifiersRepository(DatabaseConnection databaseConnection, Logger logger)
        : base(databaseConnection, logger)
    {
    }
    
    public IEnumerable<SkillModifier> GetTraitSkillModifiers(Trait trait)
    {
        return GetFiltered(x => x.TraitId == trait.Id);
    }
}