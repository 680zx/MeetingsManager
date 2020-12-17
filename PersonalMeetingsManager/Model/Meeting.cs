using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalMeetingsManager
{
    public class Meeting : ICloneable
    {
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private DateTime _reminderDateTime;

        public DateTime StartDateTime 
        { 
            get => _startDateTime;
            set
            {
                _startDateTime = value;
            }
        }
        public DateTime EndDateTime 
        {
            get => _endDateTime;
            set
            {
                _endDateTime = value;
            }
        }
    
        public DateTime ReminderDateTime 
        { 
            get => _reminderDateTime; 
            set
            {
                _reminderDateTime = value;
            }
        }

        public Meeting()
        {
        }

        public Meeting(DateTime startDateTime, DateTime endDateTime, TimeSpan reminderTime)
        {
            if (startDateTime < DateTime.Now)
                throw new ArgumentOutOfRangeException("Время встречи может быть установлено только на будущее.", nameof(startDateTime));
            if (startDateTime >= endDateTime)
                throw new ArgumentOutOfRangeException("Время начала встречи не может быть больше времени окончания.", nameof(endDateTime));

            _startDateTime = startDateTime;
            _endDateTime = endDateTime;
            _reminderDateTime = startDateTime.Subtract(reminderTime);
        }

        public object Clone()
        {
            return new Meeting
            {
                _startDateTime = this._startDateTime,
                _endDateTime = this._endDateTime,
                _reminderDateTime = this._reminderDateTime
            };
        }
    }
}
