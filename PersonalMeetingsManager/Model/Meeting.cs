using PersonalMeetingsManager.Utilities;
using System;

namespace PersonalMeetingsManager
{
    public class Meeting : ICloneable
    {
        private DateTime _start;
        private DateTime _end;
        private DateTime _reminder;

        public DateTime StartDateTime 
        { 
            get => _start;
        }
        public DateTime EndDateTime 
        {
            get => _end;
        }
        public DateTime ReminderDateTime 
        { 
            get => _reminder;
            set
            {
                _reminder = value;
            }
        }

        public Meeting()
        {
        }

        public Meeting(DateTime startDateTime, DateTime endDateTime, TimeSpan reminderTime)
        {
            if (startDateTime < DateTime.Now)
                throw new ArgumentOutOfRangeException("Время встречи может быть установлено только на будущее.");
            if (startDateTime >= endDateTime)
                throw new ArgumentOutOfRangeException("Время начала встречи не может быть больше времени окончания.");
            if (reminderTime.TotalMinutes < 0)
                throw new TimeErrorException("Невозможно установить время напоминание о встрече позже времени ее начала.");

            _start = startDateTime;
            _end = endDateTime;
            _reminder = startDateTime.Subtract(reminderTime);
        }

        public object Clone()
        {
            return new Meeting
            {
                _start = this._start,
                _end = this._end,
                _reminder = this._reminder
            };
        }
    }
}
