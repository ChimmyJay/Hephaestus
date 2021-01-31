using HephaestusDomain.Models;

namespace HephaestusDomain.Repos
{
    public interface IFocusTaskRepo
    {
        FocusTask Get();
        void Set(StartFocusingTaskDto dto);
    }
}