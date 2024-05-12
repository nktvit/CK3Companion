using CompanionData.ModelsDto;
using CompanionData.Repositories;
using CompanionDomain.Models;
using NLog;

namespace CompanionData.Services;

public class CharacterService
{
    private readonly CharacterRepository _characterRepository;
    private readonly CharacterTraitsRepository _characterTraitsRepository;
    private readonly TraitRepository _traitRepository;
    private readonly Logger _logger;

    public CharacterService(CharacterRepository characterRepository,
        CharacterTraitsRepository characterTraitsRepository, TraitRepository traitRepository, Logger logger)
    {
        _characterRepository = characterRepository;
        _characterTraitsRepository = characterTraitsRepository;
        _traitRepository = traitRepository;
        _logger = logger;
    }

    public IEnumerable<Character> GetAllCharacters()
    {
        try
        {
            _logger.Info("Starting to fetch characters...");
            var characters = _characterRepository.GetCharacters();
            foreach (var character in characters)
            {
                var traitsIds = _characterTraitsRepository.GetCharacterTraitsIdsByCharacterId(character.Id);
                var traits = _traitRepository.GetTraitsByIds(traitsIds);
                character.AddTraits(traits);
            }

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
            var character = _characterRepository.GetCharacterById(id);
            var traitsIds = _characterTraitsRepository.GetCharacterTraitsIdsByCharacterId(character.Id);
            var traits = _traitRepository.GetTraitsByIds(traitsIds);
            character.AddTraits(traits);
            return character;
        }
        catch (Exception ex)
        {
            _logger.Error($"Error fetching data: {ex.Message}");
            return new Character();
        }
    }
    public void SaveCharacter(Character character)
    {
        try
        {
            _logger.Info($"Saving character with id: {character.Id}");
            _characterRepository.SaveCharacter(character);
            _characterTraitsRepository.SaveCharacterTraits(character.Traits.Select(x => new CharacterTrait
                { CharacterId = character.Id, TraitId = x.Id }));
        }
        catch (Exception ex)
        {
            _logger.Error($"Error saving data: {ex.Message}");
        }
    }

    public void SaveCharacters(IEnumerable<Character> characters)
    {
        try
        {
            _logger.Info("Saving characters...");
            foreach (var character in characters)
            {
                _characterTraitsRepository.SaveCharacterTraits(character.Traits.Select(x => new CharacterTrait
                    { CharacterId = character.Id, TraitId = x.Id }));
            }

            _characterRepository.SaveCharacters(characters);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error saving data: {ex.Message}");
        }
    }

    public void DeleteCharacter(Character character)
    {
        try
        {
            _logger.Info($"Deleting character with id: {character.Id}");
            _characterRepository.DeleteCharacter(character);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error deleting data: {ex.Message}");
        }
    }

    public void DeleteCharacterById(string id)
    {
        try
        {
            _logger.Info($"Deleting character with id: {id}");
            _characterRepository.DeleteCharacterById(id);
        }
        catch (Exception ex)
        {
            _logger.Error($"Error deleting data: {ex.Message}");
        }
    }

    public void DeleteAllCharacters()
    {
        try
        {
            _logger.Info("Deleting all characters...");
            if (_characterRepository.GetCharacters().Any())
            {
                _characterRepository.DeleteAllCharacters();
            }
            else _logger.Warn("No characters to delete.");
               
        }
        catch (Exception ex)
        {
            _logger.Error($"Error deleting data: {ex.Message}");
        }
    }

    public void AddCharacterTrait(Character character, Trait trait)
    {
        try
        {
            _logger.Info($"Adding trait with id: {trait.Id} to character with id: {character.Id}");
            character.AddTrait(trait);
            _characterTraitsRepository.SaveCharacterTrait(new CharacterTrait
                { CharacterId = character.Id, TraitId = trait.Id });
        }
        catch (Exception ex)
        {
            _logger.Error($"Error adding trait to character: {ex.Message}");
        }
    }

    public void RemoveCharacterTrait(Character character, Trait trait)
    {
        try
        {
            _logger.Info($"Removing trait with id: {trait.Id} from character with id: {character.Id}");
            character.RemoveTrait(trait);
            _characterTraitsRepository.DeleteCharacterTrait(new CharacterTrait
                { CharacterId = character.Id, TraitId = trait.Id });
        }
        catch (Exception ex)
        {
            _logger.Error($"Error removing trait from character: {ex.Message}");
        }
    }
}