using CompanionDomain.Models;
using NLog;

namespace CompanionData.Repositories;

public class NonApplicableTraitRepository(DatabaseConnection databaseConnection)
    : DataRepository<NonApplicableTrait>(databaseConnection)
{
    public IEnumerable<NonApplicableTrait> GetTraitNonApplicableTraits(Trait trait)
    {
        return GetFiltered(x => x.TraitId == trait.Id);
    }
}