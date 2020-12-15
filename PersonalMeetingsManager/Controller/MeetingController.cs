using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalMeetingsManager
{
    class MeetingController
    {
        private List<Meeting> _meetings = new List<Meeting>();

        public void AddMeeting(Meeting newMeetingDateTime)
        {
            if (newMeetingDateTime == null)
                throw new ArgumentNullException();

            _meetings.Add(newMeetingDateTime);
        }

        public void RemoveMeeting()
        {
            throw new NotImplementedException();
        }

        public void EditMeeting()
        {
            throw new NotImplementedException();
        }

        public void CreateReminder()
        {
            throw new NotImplementedException();
        }

        public void ShowMeetings()
        {
            throw new NotImplementedException();
        }

        public void ExportMeeting()
        {
            throw new NotImplementedException();
        }
    }
}
