using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace CompanionData.Repositories;

public class DataRepository<T> where T : new()
{
    private readonly DatabaseConnection _databaseConnection;
    private readonly ILogger<DataRepository<T>> _logger;

    public DataRepository(DatabaseConnection databaseConnection, ILogger<DataRepository<T>> logger)
    {
        _databaseConnection = databaseConnection;
        _logger = logger;
        _databaseConnection.Connection.CreateTable<T>();
    }

    public void InsertOne(T item)
    {
        try
        {
            _databaseConnection.Connection.Insert(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting data into the database.");
        }
    }

    public IEnumerable<T> GetAll()
    {
        try
        {
            var data = _databaseConnection.Connection.Table<T>();
            Console.WriteLine(data.Count());
            return data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting data from the database.");
            return Enumerable.Empty<T>();
        }
    }

    public IEnumerable<T> GetFiltered(Expression<Func<T, bool>> predicate)
    {
        return _databaseConnection.Connection.Table<T>().Where(predicate);
    }
}