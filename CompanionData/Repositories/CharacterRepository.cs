using CompanionDomain.Models;
using NLog;

namespace CompanionData.Repositories;

public class CharacterRepository (DatabaseConnection databaseConnection, Logger logger)
    : DataRepository<Character>(databaseConnection, logger)
{
    public IEnumerable<Character> GetCharacters()
    {
        var data = GetAll();
        IEnumerable<Character> characters = data.ToList();
        logger.Info("Successfully retrieved {0} characters from the database.", characters.Count());
        return characters;
    }
    public Character GetCharacterById(string id)
    {
        try
        {
            return GetOne(x => x.Id == id);

        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error getting character from the database.");
            return null;
        }
    }
    public void DeleteCharacter(Character character)
    {
        Delete(character);
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
}