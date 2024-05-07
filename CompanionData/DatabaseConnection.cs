using NLog;
using SQLite;

namespace CompanionData
{
    public class DatabaseConnection(string databasePath, Logger logger) : IDisposable
    {
        private readonly SQLiteConnection _connection = new(databasePath);
        private readonly Logger _logger = logger;

        public SQLiteConnection Connection => _connection;

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}