using HephaestusDomain.Models;
using System.Collections.Generic;

namespace HephaestusDomain.Services
{
    public interface IFocusTaskTimerService
    {
        FocusTask GetFocusingTask();

        void StartFocusingTask(StartFocusingTaskDto startFocusingTaskDto);

        void StopFocusingTask();

        IEnumerable<FocusTask> GetFocusTaskHistory();
    }
}