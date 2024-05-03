using CompanionDomain.Models;

namespace CompanionData.Repositories;

public interface ITraitRepository
{
    Task<IEnumerable<Trait>> GetAllTraitsAsync();
    // Task<Trait> GetTraitByIdAsync(int customerId);
}

public class TraitRepository : ITraitRepository
{
    private readonly ISqlDataAccess _sqlDataAccess;

    public TraitRepository(ISqlDataAccess sqlDataAccess)
    {
        _sqlDataAccess = sqlDataAccess;
    }

    public async Task<IEnumerable<Trait>> GetAllTraitsAsync()
    {
        return await _sqlDataAccess.GetTraitsAsync();
    }

}