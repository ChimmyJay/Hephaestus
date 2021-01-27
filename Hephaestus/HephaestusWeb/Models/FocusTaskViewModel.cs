using System;

namespace HephaestusWeb.Models
{
    public class FocusTaskViewModel
    {
        public bool HasFocusTask { get; set; }
        public string TaskName { get; set; }
        public DateTime? TaskStartTime { get; set; }
    }
}