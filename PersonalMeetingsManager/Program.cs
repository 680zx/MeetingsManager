using System;
using System.Globalization;
using PersonalMeetingsManager.Utilities;

namespace PersonalMeetingsManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Meeting meeting = new Meeting(new DateTime(), new DateTime(), new DateTime());

            Console.WriteLine(meeting.StartDateTime);
            Console.WriteLine(meeting.EndDateTime);
            Console.WriteLine(meeting.ReminderDateTime);

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
                showMeetings(meetingController);
                TxtSaver.Save(meetingController.Meetings);
                //meetingController.RemoveMeeting(0);
            }
        }
        //12.12.2012 12:50

        /// <summary>
        /// Запрашивает у пользователя дату и время.
        /// </summary>
        /// <returns>Возвращает дату и время начала/окончания/напоминания встречи.</returns>
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

        /// <summary>
        /// Выводит список всех встреч пользователя.
        /// </summary>
        /// <param name="meetingController"></param>
        private static void showMeetings(MeetingController meetingController)
        {
            if (meetingController == null)
                throw new ArgumentNullException("Передан пустой аргумент", nameof(meetingController));

            if (meetingController.Meetings.Count == 0)
                Console.WriteLine("Сейчас в Вашем расписании нет ни одной встречи.");
            else
            {
                int counter = 1;
                foreach (Meeting meeting in meetingController.Meetings)
                {
                    Console.WriteLine($"Встреча №{counter}");
                    Console.WriteLine($"Начало:\t\t\t{meeting.StartDateTime}");
                    Console.WriteLine($"Окончание:\t\t{meeting.EndDateTime}");
                    Console.WriteLine($"Время напоминания:\t{meeting.ReminderDateTime}");
                    Console.WriteLine();
                    counter++;
                }
            }
        }
    }
}
