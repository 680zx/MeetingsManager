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
            if (newMeeting == null)
                throw new ArgumentNullException("Передан пустой аргумент", nameof(newMeeting));

            _meetings.Add(newMeeting);
        }

        public void RemoveMeeting(int index)
        {
            index--;
            if (index >= _meetings.Count || index < 0)
                throw new ArgumentOutOfRangeException("Переданный индекс находится вне границ списка", nameof(index));

            _meetings.RemoveAt(index);
        }

        public void EditMeeting()
        {
            throw new NotImplementedException();
        }

        public void CreateReminder()
        {
            throw new NotImplementedException();
        }


        public void ExportMeeting()
        {
            throw new NotImplementedException();
        }
    }
}
