﻿using System;

namespace HephaestusWeb.Models
{
    public class FocusTaskHistoryViewModel
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ElapsedTime { get; set; }
    }
}