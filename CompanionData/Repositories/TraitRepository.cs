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
        IEnumerable<Trait> traits = data.ToList();
        logger.Info("Successfully retrieved {0} traits from the database.", traits.Count());
        return traits;
    }

    public void SaveTrait(Trait trait)
    {
        InsertOne(trait);
    }
}