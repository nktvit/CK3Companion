using CompanionDomain.Enums;
using CompanionDomain.Models;
using NLog;

namespace CompanionData.Repositories;

public class SkillModifierRepository(DatabaseConnection databaseConnection)
    : DataRepository<SkillModifier>(databaseConnection)
{
    public IEnumerable<SkillModifier> GetTraitSkillModifiers(int traitId)
    {
        return GetFiltered(x => x.TraitId == traitId);
    }
}