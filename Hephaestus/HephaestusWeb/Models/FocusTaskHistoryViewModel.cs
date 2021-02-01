using System;

namespace HephaestusWeb.Models
{
    public class FocusTaskHistoryViewModel
    {
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int ElapsedTime { get; set; }
    }
}