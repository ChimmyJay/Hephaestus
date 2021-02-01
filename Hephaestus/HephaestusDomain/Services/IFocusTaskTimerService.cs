using System;
using HephaestusDomain.Models;
using System.Collections.Generic;

namespace HephaestusDomain.Services
{
    public interface IFocusTaskTimerService
    {
        FocusTask GetFocusingTask();

        void StartFocusingTask(StartFocusingTaskDto startFocusingTaskDto);

        void StopFocusingTask(DateTime endTime);

        IEnumerable<FocusTask> GetFocusTaskHistory();
    }
}