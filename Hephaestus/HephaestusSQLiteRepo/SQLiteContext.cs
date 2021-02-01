using HephaestusSQLiteRepo.Entities;
using Microsoft.EntityFrameworkCore;

namespace HephaestusSQLiteRepo
{
    public class SQLiteContext : DbContext
    {
        private readonly string _connectionString;

        public SQLiteContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<FocusTaskEntity> FocusTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}