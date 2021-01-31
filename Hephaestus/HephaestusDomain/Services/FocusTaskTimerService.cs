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
            return _focusTaskRepo.Get();
        }

        public void StartFocusingTask(StartFocusingTaskDto startFocusingTaskDto)
        {
            _focusTaskRepo.Set(startFocusingTaskDto);
        }
    }
}