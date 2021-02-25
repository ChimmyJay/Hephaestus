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
        private readonly IDateTimeProvider _dateTimeProvider;

        public FocusTaskTimerService(IFocusTaskRepo fakeRepo, IDateTimeProvider dateTimeProvider)
        {
            _focusTaskRepo = fakeRepo;
            _dateTimeProvider = dateTimeProvider;
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
                    EndTime = x.EndTime,
                    ElapsedTime = (int)Math.Floor((x.EndTime - x.StartTime).TotalSeconds)
                }).OrderByDescending(x => x.EndTime);
        }
    }
}