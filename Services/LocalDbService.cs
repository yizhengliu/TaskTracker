using SQLite;
using TaskTracker.Model;

namespace TaskTracker.Services
{
    internal class LocalDbService
    {
        private const string DB_NAME = "TaskTracker.db3";
        private const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;
        private static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new(DatabasePath);
            _connection.CreateTableAsync<Project>();
        }

        public async Task<List<Project>> GetProjectsAsync() 
        {
            return await _connection.Table<Project>().ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(long id)
        {
            return await _connection.Table<Project>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateProject(Project project)
        {
            await _connection.InsertAsync(project);
        }

        public async Task UpdateProject(Project project)
        {
            await _connection.UpdateAsync(project);
        }

        public async Task DeleteProject(Project project)
        {
            await _connection.DeleteAsync(project);
        }
    }
}
