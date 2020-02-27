using System;
using System.Collections.Generic;
using System.Text;

namespace LineOperator2.Models
{
    public class Event
    {
        public string Name { get; set; }
        public double Duration { get; set; }
        public int DisplayDuration { get => (int)Duration; }
        public DateTime StartTime { get; set; }
    }
}
