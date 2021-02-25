using System;

namespace HephaestusDomain.Models
{
    public class FocusTask
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int ElapsedTime() => (int)Math.Floor((EndTime - StartTime).TotalSeconds);
    }
}