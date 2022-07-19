using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Status { get; set; }
    }
}
