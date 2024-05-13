using System.Linq.Expressions;
using CompanionDomain.Models;
using NLog;

namespace CompanionData.Repositories;

public class CharacterRepository(DatabaseConnection databaseConnection)
    : DataRepository<Character>(databaseConnection)
{
    public IEnumerable<Character> GetCharacters()
    {
        var data = GetAll();
        IEnumerable<Character> characters = data.ToList();
        return characters;
    }

    public List<TResult> GetCharacterPropertyValues<TResult>(Func<Character, TResult> propertySelector)
    {
        var data = GetAll();
        var characters = data.ToList();

        return characters.Select(propertySelector).ToList();
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