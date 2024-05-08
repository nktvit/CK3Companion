using CompanionDomain.Models;
using NLog;

namespace CompanionData.Repositories;

public class NonApplicableTraitRepository(DatabaseConnection databaseConnection, Logger logger)
    : DataRepository<NonApplicableTrait>(databaseConnection, logger)
{
    public IEnumerable<NonApplicableTrait> GetTraitNonApplicableTraits(Trait trait)
    {
        try
        {
            return GetFiltered(x => x.TraitId == trait.Id);

        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error getting non-applicable traits from the database.");
            return Enumerable.Empty<NonApplicableTrait>();

        }
    }
}