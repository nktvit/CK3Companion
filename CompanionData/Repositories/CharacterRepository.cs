using CompanionDomain.Models;
using NLog;

namespace CompanionData.Repositories;

public class CharacterRepository(DatabaseConnection databaseConnection, Logger logger)
    : DataRepository<Character>(databaseConnection, logger)
{
    public IEnumerable<Character> GetCharacters()
    {
        var data = GetAll();
        IEnumerable<Character> characters = data.ToList();
        return characters;
    }

    public Character GetCharacterById(string id)
    {
        return GetOne(x => x.Id == id);
    }

    public void DeleteCharacter(Character character)
    {
        DeleteOne(character);
    }

    public void DeleteCharacterById(string id)
    {
        DeleteById(id);
    }

    public void DeleteAllCharacters()
    {
        DeleteAll();
    }

    public void SaveCharacter(Character character)
    {
        InsertOne(character);
    }
    public void SaveCharacters(IEnumerable<Character> characters)
    {
        InsertAll(characters);
    }

}