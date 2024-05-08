using CompanionDomain.Enums;
using CompanionDomain.Models;
using NLog;

namespace CompanionData.Repositories;

public class SkillModifierRepository(DatabaseConnection databaseConnection, Logger logger)
    : DataRepository<SkillModifier>(databaseConnection, logger)
{
    public IEnumerable<SkillModifier> GetTraitSkillModifiers(Trait trait)
    {
        try
        {
            return GetFiltered(x => x.TraitId == trait.Id);
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error getting skill modifiers from the database.");
            return Enumerable.Empty<SkillModifier>();
        }
    }
}