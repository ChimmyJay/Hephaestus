using HephaestusDomain.Models;

namespace HephaestusDomain.Services
{
    public interface IFocusTaskTimerService
    {
        FocusTask GetFocusingTask();
        void StartFocusingTask(StartFocusingTaskDto startFocusingTaskDto);
        void StopFocusingTask();
    }
}