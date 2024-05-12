using CompanionDomain.Interfaces;
using CompanionDomain.Models;
using NLog;

namespace CompanionData.Repositories;

public class TraitRepository(DatabaseConnection databaseConnection, Logger logger)
    : DataRepository<Trait>(databaseConnection, logger)
{
    public IEnumerable<Trait> GetTraits()
    {
        var data = GetAll();
        return data.ToList();
    }
    public Trait GetTraitById(int id)
    {
        return GetOne(x => x.Id == id);
    }
    public IEnumerable<Trait> GetTraitsByIds(IEnumerable<int> ids)
    {
        return GetFiltered(x => ids.Contains(x.Id));
    }

    public void SaveTrait(Trait trait)
    {
        InsertOne(trait);
    }
}