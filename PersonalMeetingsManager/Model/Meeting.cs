using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalMeetingsManager
{
    public struct Meeting
    {
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private DateTime _reminderDateTime;

        public DateTime StartDateTime { get => _startDateTime; }
        public DateTime EndDateTime { get => _endDateTime; }
        public DateTime ReminderDateTime 
        { 
            get => _reminderDateTime; 
            set
            {
                _reminderDateTime = value;
            }
        }

        public Meeting(DateTime startDateTime, DateTime endDateTime, DateTime reminderTime)
        {
            _startDateTime = startDateTime;
            _endDateTime = endDateTime;
            _reminderDateTime = reminderTime;
        }

    }
}
