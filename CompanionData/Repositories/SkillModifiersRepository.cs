using CompanionDomain.Enums;
using CompanionDomain.Models;
using NLog;

namespace CompanionData.Repositories;

public class SkillModifierRepository(DatabaseConnection databaseConnection, Logger logger)
    : DataRepository<SkillModifier>(databaseConnection, logger)
{
    public IEnumerable<SkillModifier> GetTraitSkillModifiers(int traitId)
    {
        return GetFiltered(x => x.TraitId == traitId);
    }
}