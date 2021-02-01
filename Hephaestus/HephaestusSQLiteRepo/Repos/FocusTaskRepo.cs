using HephaestusDomain.Models;
using HephaestusDomain.Repos;
using HephaestusSQLiteRepo.Entities;
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
                : new FocusTask { Name = entity.Name, StartTime = entity.StartTime.UtcDateTime };
        }

        public void Set(StartFocusingTaskDto dto)
        {
            var focusTaskEntity = _context.FocusTasks.SingleOrDefault();
            if (focusTaskEntity != null)
            {
                _context.FocusTasks.Remove(focusTaskEntity);
            }

            _context.FocusTasks.Add(new FocusTaskEntity()
            {
                Name = dto.Name,
                StartTime = dto.StartTime
            });
            _context.SaveChanges();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}