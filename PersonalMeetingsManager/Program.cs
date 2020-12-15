using System;
using System.Globalization;

namespace PersonalMeetingsManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Meeting meeting = new Meeting(new DateTime(), new DateTime(), new DateTime());

            Console.WriteLine(meeting.StartDateTime);
            Console.WriteLine(meeting.EndDateTime);
            Console.WriteLine(meeting.ReminderTime);

            MeetingController meetingController = new MeetingController();

            while(true)
            {
                Console.WriteLine("Вы хотели бы добавить новую встречу?");
                Console.WriteLine("Введите дату и время начала предстоящей встречи (dd.MM.yyyy hh:mm):\n");
                var startTime = readDataTime();

                Console.WriteLine("Введите дату и время окончания предстоящей встречи (dd.MM.yyyy hh:mm):\n");
                var endTime = readDataTime();

                Console.WriteLine("Введите время за сколько вам нужно напомнить о предстоящей встрече (dd.MM.yyyy hh:mm):\n");
                var reminderTime = readDataTime();

                meetingController.AddMeeting(new Meeting(startTime, endTime, reminderTime));
            }
        }
        
        private static DateTime readDataTime()
        {
            string input;
            DateTime dateTime;

            do
            {
                input = Console.ReadLine();
            } 
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy hh:mm", null, DateTimeStyles.None, out dateTime));

            return dateTime;
        }
    }
}
