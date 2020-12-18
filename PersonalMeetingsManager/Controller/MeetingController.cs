﻿using PersonalMeetingsManager.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PersonalMeetingsManager
{
    public class MeetingController
    {
        private List<Meeting> _meetings = new List<Meeting>();
        private DateTime _nextReminderDateTime;
        private Meeting _nextMeeting;
        public List<Meeting> Meetings { get => _meetings; }
        public DateTime NextReminderDateTime { get => _nextReminderDateTime; }
        public Meeting NextMeeting { get => _nextMeeting; }
        

        // TODO: добавить напоминание о встрече
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
                    throw new MeetingCrossingException("Невозможно добавить новую встречу - пересечение с уже существующей встречей.", nameof(newMeeting));
                }
            }
            _meetings.Add(newMeeting);
            setNextReminderDateTime();
        }

        /// <summary>
        /// Удаляет встречу из списка встреч по индексу <see cref="index"/>.
        /// </summary>
        /// <param name="index">Индекс удаляемой встречи.</param>
        public void RemoveMeeting(int index)
        {
            if (index >= _meetings.Count || index < 0)
                throw new ArgumentOutOfRangeException("Переданный индекс встречи находится вне границ списка", nameof(index));

            _meetings.RemoveAt(index);
            setNextReminderDateTime();
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
                throw new ArgumentOutOfRangeException("Переданный индекс встречи находится вне границ списка.", nameof(index));

            _meetings[index] = changedMeeting.Clone() as Meeting;
            setNextReminderDateTime();
        }

        /// <summary>
        /// Изменяет время напоминания о встрече по индексу <see cref="index"/>.
        /// </summary>
        /// <param name="index">Индекс изменяемой встречи.</param>
        /// <param name="dateTime">Новое время напоминания.</param>
        public void ChangeReminderTime(int index, TimeSpan changedTimeSpan)
        {
            if (changedTimeSpan == null)
                throw new ArgumentNullException("Передан null в качестве параметра.", nameof(changedTimeSpan));
            if (index >= _meetings.Count || index < 0)
                throw new ArgumentOutOfRangeException("Переданный индекс встречи находится вне границ списка", nameof(index));
            if (changedTimeSpan.TotalMinutes < 0)
                throw new TimeErrorException("Невозможно установить время напоминание о встрече позже времени ее начала.", nameof(changedTimeSpan));

            var currentMeeting = _meetings[index];
            currentMeeting.ReminderDateTime = currentMeeting.StartDateTime.Subtract(changedTimeSpan);
            setNextReminderDateTime();
        }
        
        private void setNextReminderDateTime()
        {
            _nextReminderDateTime = DateTime.MaxValue;
            for (int i = 0; i < _meetings.Count; i++)
            {
                if (_meetings[i].ReminderDateTime > DateTime.Now && _meetings[i].ReminderDateTime < _nextReminderDateTime)
                {
                    _nextReminderDateTime = _meetings[i].ReminderDateTime;
                    _nextMeeting = _meetings[i];
                }
            }
        }

        
    }
}
