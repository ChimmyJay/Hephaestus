using HephaestusDomain.Models;
using System.Collections.Generic;

namespace HephaestusDomain.Repos
{
    public interface IFocusTaskRepo
    {
        FocusTask GetFocusing();

        void Set(StartFocusingTaskDto dto);

        void Clear();

        IEnumerable<FocusTask> GetHistory();
    }
}