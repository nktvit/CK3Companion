using CompanionDomain.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CompanionData;

public interface ISqlDataAccess
{
    Task<IEnumerable<Trait>> GetTraitsAsync();
}

public class SqlDataAccess : ISqlDataAccess
{
    private const string ConnectionId = "Default"; // Use const for fixed values
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<Trait>> GetTraitsAsync()
    {
        using var connection = new SqliteConnection(_config.GetConnectionString(ConnectionId));
        connection.Open();

        // Execute SQL query to retrieve traits
        var traits = await connection.QueryAsync<Trait>("SELECT * FROM Trait");

        return traits;
    }
}