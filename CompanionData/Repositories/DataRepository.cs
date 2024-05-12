using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using NLog;
using SQLite;

namespace CompanionData.Repositories;

public class DataRepository<T> where T : new()
{
    private readonly DatabaseConnection _databaseConnection;
    private readonly Logger _logger;

    public DataRepository(DatabaseConnection databaseConnection, Logger logger)
    {
        _databaseConnection = databaseConnection;
        _logger = logger;
        _databaseConnection.Connection.CreateTable<T>();
    }

    public IEnumerable<T> GetAll()
    {
        var data = _databaseConnection.Connection.Table<T>();
        Console.WriteLine(data.Count());
        return data;
    }

    public T GetOne(Expression<Func<T, bool>> predicate)
    {
        return _databaseConnection.Connection.Table<T>().FirstOrDefault(predicate);
    }

    public IEnumerable<T> GetFiltered(Expression<Func<T, bool>> predicate)
    {
        return _databaseConnection.Connection.Table<T>().Where(predicate);
    }

    public IEnumerable<TJoined> GetJoinedEntities<TJoined>(string query, object parameters = null) where TJoined : new()
    {
        return _databaseConnection.Connection.Query<TJoined>(query, parameters);
    }


    public void InsertOne(T item)
    {
        _databaseConnection.Connection.Insert(item);
    }

    public void InsertAll(IEnumerable<T> items)
    {
        _databaseConnection.Connection.InsertAll(items);
    }

    public void DeleteOne(T item)
    {
        _databaseConnection.Connection.Delete(item);
    }

    public void DeleteById(object id)
    {
        _databaseConnection.Connection.Delete<T>(id);
    }

    public void DeleteAll()
    {
        _databaseConnection.Connection.DeleteAll<T>();
    }
}