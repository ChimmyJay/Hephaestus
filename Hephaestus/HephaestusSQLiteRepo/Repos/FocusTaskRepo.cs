using System;
using System.Collections.Generic;
using System.Linq;
using HephaestusDomain.Models;
using HephaestusDomain.Repos;
using HephaestusSQLiteRepo.Entities;

namespace HephaestusSQLiteRepo.Repos
{
    public class FocusTaskRepo : IFocusTaskRepo
    {
        private readonly SQLiteContext _context;

        public FocusTaskRepo(SQLiteContext sqLiteContext)
        {
            _context = sqLiteContext;
        }

        public FocusTask GetFocusing()
        {
            var entity = _context.FocusTasks
                .SingleOrDefault(x => !x.EndTime.HasValue);
            return entity == null
                ? null
                : new FocusTask { Name = entity.Name, StartTime = entity.StartTime.UtcDateTime };
        }

        public void StartFocusing(StartFocusingTaskDto dto)
        {
            _context.FocusTasks.Add(new FocusTaskEntity()
            {
                Name = dto.Name,
                StartTime = dto.StartTime
            });
            _context.SaveChanges();
        }

        public void StopFocusing(DateTime endTime)
        {
            var entity = _context.FocusTasks
                .Single(x => !x.EndTime.HasValue);
            entity.EndTime = endTime;
            _context.Update(entity);
            _context.SaveChanges();
        }

        public IEnumerable<FocusTask> GetHistory()
        {
            return _context.FocusTasks
                .Where(x => x.EndTime.HasValue)
                .Select(x => new FocusTask
                {
                    Name = x.Name,
                    StartTime = x.StartTime.UtcDateTime,
                    EndTime = x.EndTime.Value.UtcDateTime,
                }).ToList();
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return _context.Categories
                .Select(x => new Category
                {
                    Name = x.Name
                });
        }
    }
}