using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalMeetingsManager.Utilities
{
    public class MeetingCrossingException : ArgumentException
    {
        public MeetingCrossingException(string message) : base(message) { }
        public MeetingCrossingException(string message, string paramName) : base(message, paramName) { }
        public MeetingCrossingException(string message, Exception inner) : base(message, inner) { }
    }
    public class TimeErrorException : ArgumentException
    {
        public TimeErrorException(string message) : base(message) { }
        public TimeErrorException(string message, string paramName) : base(message, paramName) { }
        public TimeErrorException(string message, Exception inner) : base(message, inner) { }
    }         
    public class SettingDateTimeException : ArgumentOutOfRangeException
    {
        public SettingDateTimeException(string message) : base(message) { }
        public SettingDateTimeException(string message, string paramName) : base(message, paramName) { }
        public SettingDateTimeException(string message, Exception inner) : base(message, inner) { }
    }
}
