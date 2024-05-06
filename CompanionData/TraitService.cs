using CompanionDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanionData
{
    public class TraitService
    {
        private readonly ApplicationDbContext _dbContext;

        public TraitService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Trait>> GetAllTraits()
        {
            return await _dbContext.Traits.ToListAsync();
        }

        public async Task AddTrait(Trait trait)
        {
            _dbContext.Traits.Add(trait);
            await _dbContext.SaveChangesAsync();
        }

        // Add other CRUD methods as needed
    }
}