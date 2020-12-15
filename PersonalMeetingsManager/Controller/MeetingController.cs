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

        public void AddMeeting(Meeting newMeeting)
        {
            _meetings.Add(newMeeting);
        }

        public void RemoveMeeting(int index)
        {
            index--;
            if (index >= _meetings.Count || index < 0)
                throw new ArgumentOutOfRangeException("Переданный индекс находится вне границ списка", nameof(index));

            _meetings.RemoveAt(index);
        }

        public void EditMeeting(int index, Meeting changedMeeting)
        {
            index--;
            if (index >= _meetings.Count || index < 0)
                throw new ArgumentOutOfRangeException("Переданный индекс находится вне границ списка", nameof(index));

            _meetings[index] = changedMeeting;
        }

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
