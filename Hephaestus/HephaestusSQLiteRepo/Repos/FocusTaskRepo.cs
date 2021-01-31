using HephaestusDomain.Models;
using HephaestusDomain.Repos;
using System.Linq;

namespace HephaestusSQLiteRepo.Repos
{
    public class FocusTaskRepo : IFocusTaskRepo
    {
        private readonly SQLiteContext _context;

        public FocusTaskRepo(SQLiteContext sqLiteContext)
        {
            _context = sqLiteContext;
        }

        public FocusTask Get()
        {
            var entity = _context.FocusTasks.SingleOrDefault();
            return entity == null
                ? null
                : new FocusTask { Name = entity.Name, StartTime = entity.StartTime };
        }

        public void Set(StartFocusingTaskDto startFocusingTaskDto)
        {
        }
    }
}