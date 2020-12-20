using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalMeetingsManager.Utilities
{
    class Exceptions
    {
    }

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
}
