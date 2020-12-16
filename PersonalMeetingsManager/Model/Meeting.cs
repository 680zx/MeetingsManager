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
            if (startDateTime < DateTime.Now)
                throw new ArgumentOutOfRangeException("Время встречи может быть установлено только на будущее.", nameof(startDateTime));

            if (startDateTime >= endDateTime)
                throw new ArgumentOutOfRangeException("Время начала встречи не может быть больше времени окончания.", nameof(endDateTime));

            _startDateTime = startDateTime;
            _endDateTime = endDateTime;
            _reminderDateTime = reminderTime;
        }

    }
}
