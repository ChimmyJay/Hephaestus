using System;
using System.Collections;
using System.Collections.Generic;
using HephaestusDomain.Models;

namespace HephaestusDomain.Repos
{
    public interface IFocusTaskRepo
    {
        FocusTask GetFocusing();

        void StartFocusing(StartFocusingTaskDto dto);

        void StopFocusing(DateTime endTime);

        IEnumerable<FocusTask> GetHistory();

        IEnumerable<Category> GetAllCategory();
    }
}