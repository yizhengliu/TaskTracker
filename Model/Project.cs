using SQLite;

namespace TaskTracker.Model
{
    public class Project
    {
        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }
        public string? ProjectName { get; set; }
    }
}
