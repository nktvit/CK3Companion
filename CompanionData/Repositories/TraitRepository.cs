using CompanionDomain.Interfaces;
using CompanionDomain.Models;
using Microsoft.Extensions.Logging;

namespace CompanionData.Repositories;

public class TraitRepository(DatabaseConnection databaseConnection, ILogger<TraitRepository> logger)
    : DataRepository<Trait>(databaseConnection, logger)
{
    public IEnumerable<Trait> GetTraits()
    {
        return GetAll();
    }

    public void SaveTrait(Trait trait)
    {
        InsertOne(trait);
    }
}