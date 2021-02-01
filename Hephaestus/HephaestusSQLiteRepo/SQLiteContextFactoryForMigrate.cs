using Microsoft.EntityFrameworkCore.Design;

namespace HephaestusSQLiteRepo
{
    public class SQLiteContextFactoryForMigrate : IDesignTimeDbContextFactory<SQLiteContext>
    {
        public SQLiteContext CreateDbContext(string[] args)
        {
            return new SQLiteContext("Data Source=Hephaestus.db");
        }
    }
}