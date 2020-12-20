using PersonalMeetingsManager.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PersonalMeetingsManager
{
    public delegate void MeetingStateHandler(string message);
    public class MeetingController
    {
        private List<Meeting> _meetings = new List<Meeting>();
        private static DateTime _nextRemind;
        private static Meeting _nextMeeting;
        private Timer _timer = new Timer(Remind);

        public List<Meeting> Meetings { get => _meetings; }        
        public static event MeetingStateHandler Notify;
        
        public MeetingController()
        {
        }

        /// <summary>
        /// Добавляет <see cref="newMeeting"/> в список встреч.
        /// </summary>
        /// <param name="newMeeting">Новая встреча.</param>
        public void AddMeeting(Meeting newMeeting)
        {
            if (newMeeting == null)
                throw new ArgumentNullException("Передан null в качестве параметра.", nameof(newMeeting));
            foreach (Meeting meeting in _meetings)
            {
                if (newMeeting.StartDateTime >= meeting.StartDateTime && newMeeting.StartDateTime < meeting.EndDateTime ||
                    newMeeting.EndDateTime > meeting.StartDateTime && newMeeting.EndDateTime < meeting.EndDateTime || 
                    newMeeting.StartDateTime <= meeting.StartDateTime && newMeeting.EndDateTime >= meeting.EndDateTime)
                {
                    throw new MeetingCrossingException("Невозможно добавить новую встречу - пересечение с уже существующей встречей.");
                }
            }
            _meetings.Add(newMeeting);
            Notify?.Invoke("Добавлена новая встреча.");
            setNextReminderDateTime();
            _timer.Change(_nextRemind.Subtract(DateTime.Now), Timeout.InfiniteTimeSpan);
        }

        /// <summary>
        /// Удаляет встречу из списка встреч по индексу <see cref="index"/>.
        /// </summary>
        /// <param name="index">Индекс удаляемой встречи.</param>
        public void RemoveMeeting(int index)
        {
            if (index >= _meetings.Count || index < 0)
                throw new ArgumentOutOfRangeException("Переданный индекс встречи находится вне границ списка");

            _meetings.RemoveAt(index);
            Notify?.Invoke("Встреча удалена из списка.");
            setNextReminderDateTime();
            _timer.Change(_nextRemind.Subtract(DateTime.Now), Timeout.InfiniteTimeSpan);
        }

        /// <summary>
        /// Изменяет встречу по индексу <see cref="index"/>.
        /// </summary>
        /// <param name="index">Индекс удаляемой встречи.</param>
        /// <param name="changedMeeting">Изменнная встреча.</param>
        public void EditMeeting(int index, Meeting changedMeeting)
        {
            if (changedMeeting == null)
                throw new ArgumentNullException("Передан null в качестве параметра.", nameof(changedMeeting));
            if (index >= _meetings.Count || index < 0)
                throw new ArgumentOutOfRangeException("Переданный индекс встречи находится вне границ списка.");

            _meetings[index] = changedMeeting.Clone() as Meeting;
            Notify?.Invoke("Данные встречи изменены.");
            setNextReminderDateTime();
            _timer.Change(_nextRemind.Subtract(DateTime.Now), Timeout.InfiniteTimeSpan);
        }

        /// <summary>
        /// Изменяет время напоминания о встрече по индексу <see cref="index"/>.
        /// </summary>
        /// <param name="index">Индекс изменяемой встречи.</param>
        /// <param name="dateTime">Новое время напоминания.</param>
        public void EditReminderTime(int index, TimeSpan changedTimeSpan)
        {
            if (index >= _meetings.Count || index < 0)
                throw new ArgumentOutOfRangeException("Переданный индекс встречи находится вне границ списка");
            if (changedTimeSpan.TotalMinutes < 0)
                throw new TimeErrorException("Невозможно установить время напоминание о встрече позже времени ее начала.");

            var currentMeeting = _meetings[index];
            currentMeeting.ReminderDateTime = currentMeeting.StartDateTime.Subtract(changedTimeSpan);
            Notify?.Invoke("Время напоминания о встрече изменено.");
            setNextReminderDateTime();
            _timer.Change(_nextRemind.Subtract(DateTime.Now), Timeout.InfiniteTimeSpan);
        }
        
        private bool setNextReminderDateTime()
        {
            _nextRemind = DateTime.MaxValue;
            _nextMeeting = null;
            for (int i = 0; i < _meetings.Count; i++)
            {
                if (_meetings[i].ReminderDateTime > DateTime.Now && _meetings[i].ReminderDateTime < _nextRemind)
                {
                    _nextRemind = _meetings[i].ReminderDateTime;
                    _nextMeeting = _meetings[i];
                }
            }

            return _nextMeeting != null ? true : false;
            //_nextRemind = _meetings[0].ReminderDateTime;
            //_nextMeeting = _meetings[0];
        }

        private static void Remind(object state)
        {
            Notify?.Invoke($"\aНапоминание о предстоящей встрече {_nextMeeting.StartDateTime.Date.ToString("D")}: \n" +
                           $"start:\t{_nextMeeting.StartDateTime}\n" +
                           $"remind:\t{_nextMeeting.EndDateTime}");
        }

    }
}
