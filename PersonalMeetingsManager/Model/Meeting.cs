using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalMeetingsManager
{
    public class Meeting
    {
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public DateTime reminderTime { get; set; }
    }
}
