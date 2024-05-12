using NLog;
using SQLite;

namespace CompanionData
{
    public class DatabaseConnection(string databasePath) : IDisposable
    {
        private readonly SQLiteConnection _connection = new(databasePath);
        public SQLiteConnection Connection => _connection;

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}