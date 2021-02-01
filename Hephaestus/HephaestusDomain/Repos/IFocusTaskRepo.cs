using System;
using HephaestusDomain.Models;
using System.Collections.Generic;

namespace HephaestusDomain.Repos
{
    public interface IFocusTaskRepo
    {
        FocusTask GetFocusing();

        void StartFocusing(StartFocusingTaskDto dto);

        void StopFocusing(DateTime endTime);

        IEnumerable<FocusTask> GetHistory();
    }
}