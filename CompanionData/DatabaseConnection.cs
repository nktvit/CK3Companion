using Microsoft.Extensions.Logging;
using SQLite;

namespace CompanionData
{
    public class DatabaseConnection(string databasePath, ILogger<DatabaseConnection> logger) : IDisposable
    {
        private readonly SQLiteConnection _connection = new(databasePath);
        private readonly ILogger<DatabaseConnection> _logger = logger;

        public SQLiteConnection Connection => _connection;

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}