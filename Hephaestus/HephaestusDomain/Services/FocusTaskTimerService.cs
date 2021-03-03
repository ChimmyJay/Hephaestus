using System;
using System.Collections.Generic;
using System.Linq;
using HephaestusDomain.Models;
using HephaestusDomain.Repos;

namespace HephaestusDomain.Services
{
    public class FocusTaskTimerService : IFocusTaskTimerService
    {
        private readonly IFocusTaskRepo _focusTaskRepo;

        public FocusTaskTimerService(IFocusTaskRepo fakeRepo)
        {
            _focusTaskRepo = fakeRepo;
        }

        public FocusTask GetFocusingTask()
        {
            return _focusTaskRepo.GetFocusing();
        }

        public void StartFocusingTask(StartFocusingTaskDto startFocusingTaskDto)
        {
            _focusTaskRepo.StartFocusing(startFocusingTaskDto);
        }

        public void StopFocusingTask(DateTime endTime)
        {
            _focusTaskRepo.StopFocusing(endTime);
        }

        public IEnumerable<FocusTask> GetFocusTaskHistory()
        {
            return _focusTaskRepo.GetHistory()
                .Select(x => new FocusTask()
                {
                    Name = x.Name,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime
                }).OrderByDescending(x => x.EndTime);
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return _focusTaskRepo.GetAllCategory();
        }
    }
}