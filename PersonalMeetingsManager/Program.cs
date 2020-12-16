using System;
using System.Globalization;
using PersonalMeetingsManager.Utilities;
using System.Collections.Generic;

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
                TxtSaver.Save(meetingController.Meetings);
                Console.WriteLine("Вы хотели бы добавить новую встречу?");
                Console.WriteLine("Введите дату и время начала предстоящей встречи (dd.MM.yyyy hh:mm):\n");
                var startTime = readDateTime();

                Console.WriteLine("Введите дату и время окончания предстоящей встречи (dd.MM.yyyy hh:mm):\n");
                var endTime = readDateTime();

                Console.WriteLine("Введите время за сколько вам нужно напомнить о предстоящей встрече (dd.MM.yyyy hh:mm):\n");
                var reminderTime = readDateTime();

                meetingController.AddMeeting(new Meeting(startTime, endTime, reminderTime));
                showMeetings(meetingController.Meetings);
                
                //meetingController.RemoveMeeting(0);
            }
        }
        //12.12.2012 12:50

        /// <summary>
        /// Запрашивает у пользователя дату и время.
        /// </summary>
        /// <returns>Возвращает дату и время начала/окончания/напоминания встречи.</returns>
        private static DateTime readDateTime()
        {
            string input;
            DateTime dateTime;

            // TODO: добавить сообщение пользователю об ошибке, если неправильно введена дата.
            do
            {
                input = Console.ReadLine();
            } 
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy hh:mm", null, DateTimeStyles.None, out dateTime));

            return dateTime;
        }

        /// <summary>
        /// Выводит список всех встреч пользователя <see cref="items">.
        /// </summary>
        /// <param name="items"></param>
        private static void showMeetings(List<Meeting> items)
        {
            if (items == null)
                throw new ArgumentNullException("Список встреч не может быть null.", nameof(items));

            if (items.Count == 0)
                Console.WriteLine("Сейчас в Вашем расписании нет ни одной встречи.");
            else
            {
                int counter = 1;
                foreach (Meeting meeting in items)
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
