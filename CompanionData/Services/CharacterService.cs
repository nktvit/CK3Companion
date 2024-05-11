using CompanionData.Repositories;
using CompanionDomain.Models;
using NLog;

namespace CompanionData.Services;

public class CharacterService
{
    private readonly CharacterRepository _characterRepository;
    private readonly Logger _logger;

    public CharacterService(CharacterRepository characterRepository, Logger logger)
    {
        _characterRepository = characterRepository;
        _logger = logger;
    }
    
    public IEnumerable<Character> GetAllCharacters()
    {
        try
        {
            _logger.Info("Starting to fetch characters...");
            var characters = _characterRepository.GetCharacters();
            _logger.Info("Finished fetching characters.");
            return characters;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error fetching data: {ex.Message}");
            return Enumerable.Empty<Character>();
        }
    }
    
    public Character GetCharacterById(string id)
    {
        try
        {
            _logger.Info($"Fetching character with id: {id}");
            return _characterRepository.GetCharacterById(id);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error fetching data: {ex.Message}");
            return new Character();
        }
      
    }
}