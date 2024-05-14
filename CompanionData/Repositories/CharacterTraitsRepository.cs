using CompanionData.ModelsDto;
using CompanionDomain.Models;
using NLog;

namespace CompanionData.Repositories;

public class CharacterTraitsRepository(DatabaseConnection databaseConnection)
    : DataRepository<CharacterTrait>(databaseConnection)
{
    public IEnumerable<CharacterTrait> GetAllRecords()
    {
        var data = GetAll();
        return data.ToList();
    }

    // Get all character traits
    public List<int> GetCharacterTraitsIdsByCharacterId(string characterId)
    {
        var data = GetFiltered(x => x.CharacterId == characterId);
        return data.Select(x => x.TraitId).ToList();
    }

    public List<int> GetCharacterTraitsIds(Character character)
    {
        var data = GetFiltered(x => x.CharacterId == character.Id);
        return data.Select(x => x.TraitId).ToList();
    }

    public void SaveCharacterTrait(CharacterTrait characterTrait)
    {
        InsertOne(characterTrait);
    }

    public void SaveCharacterTraits(IEnumerable<CharacterTrait> characterTraits)
    {
        InsertAll(characterTraits);
    }

    public void DeleteCharacterTrait(CharacterTrait characterTrait)
    {
        DeleteOne(characterTrait);
    }

    public void DeleteAllCharacterTraits()
    {
        DeleteAll();
    }
}