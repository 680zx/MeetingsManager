using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalMeetingsManager
{
    public class Meeting
    {
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private DateTime _reminderDateTime;

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime ReminderTime { get; set; }

        public Meeting(DateTime startDateTime, DateTime endDateTime, DateTime reminderTime)
        {
            _startDateTime = startDateTime;
            _endDateTime = endDateTime;
            _reminderDateTime = reminderTime;
        }
    }
}
