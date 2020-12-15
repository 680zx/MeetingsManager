using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalMeetingsManager
{
    public class MeetingController
    {
        private List<Meeting> _meetings = new List<Meeting>();
        public List<Meeting> Meetings { get => _meetings; }

        public MeetingController()
        {
        }

        /// <summary>
        /// Добавляет <see cref="newMeeting"/> в список встреч.
        /// </summary>
        /// <param name="newMeeting">Новая встреча.</param>
        public void AddMeeting(Meeting newMeeting)
        {
            _meetings.Add(newMeeting);
        }

        /// <summary>
        /// Удаляет встречу из списка встреч по индексу <see cref="index"/>.
        /// </summary>
        /// <param name="index">Индекс удаляемой встречи.</param>
        public void RemoveMeeting(int index)
        {
            index--;
            if (index >= _meetings.Count || index < 0)
                throw new ArgumentOutOfRangeException("Переданный индекс находится вне границ списка", nameof(index));

            _meetings.RemoveAt(index);
        }

        /// <summary>
        /// Изменяет встречу по индексу <see cref="index"/>.
        /// </summary>
        /// <param name="index">Индекс удаляемой встречи.</param>
        /// <param name="changedMeeting">Изменнная встреча.</param>
        public void EditMeeting(int index, Meeting changedMeeting)
        {
            index--;
            if (index >= _meetings.Count || index < 0)
                throw new ArgumentOutOfRangeException("Переданный индекс находится вне границ списка", nameof(index));

            _meetings[index] = changedMeeting;
        }

        /// <summary>
        /// Изменяет время напоминания о встрече по индексу <see cref="index"/>.
        /// </summary>
        /// <param name="index">Индекс изменяемой встречи.</param>
        /// <param name="dateTime">Новое время напоминания.</param>
        public void ChangeReminderTime(int index, DateTime dateTime)
        {
            index--;
            if (index >= _meetings.Count || index < 0)
                throw new ArgumentOutOfRangeException("Переданный индекс находится вне границ списка", nameof(index));

            var currentMeeting = _meetings[index];
            currentMeeting.ReminderDateTime = dateTime;
        }


        public void ExportMeeting()
        {
            throw new NotImplementedException();
        }
    }
}
