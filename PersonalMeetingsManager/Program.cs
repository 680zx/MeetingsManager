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
            MeetingController meetingController = new MeetingController();
            
            while(true)
            {
                Console.Clear();
                Console.WriteLine("\tMeetings Manager");
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("N - добавить новую встречу");    
                Console.WriteLine("E - редактировать список встреч");    
                Console.WriteLine("R - изменить напоминание о встрече");    
                Console.WriteLine("S - вывести список встреч на экран");    
                Console.WriteLine("P - экспорт встреч в текстовый файл");    
                Console.WriteLine("H - справка");    
                Console.WriteLine("Q - выход");
                
                var userInput = Console.ReadKey();
                switch(userInput.Key)
                {
                    case ConsoleKey.N:
                        Console.Clear();
                        var newMeeting = EnterNewMeeting();
                        meetingController.AddMeeting(newMeeting);
                        break;
                }
            }
        }
        //12.12.2012 12:50

        /// <summary>
        /// Создает новую встречу.
        /// </summary>
        /// <returns>Возвращает новую встречу.</returns>
        private static Meeting EnterNewMeeting()
        {
            var startDateTime = readDateTime("начала предстоящей встречи");
            var endDateTime = readDateTime("окончания предстоящей встречи");
            var reminderDateTime = readDateTime("напоминания о предстоящей встрече");
            return new Meeting(startDateTime, endDateTime, reminderDateTime);
        }

        /// <summary>
        /// Запрашивает у пользователя дату и время.
        /// </summary>
        /// <returns>Возвращает дату и время начала/окончания/напоминания встречи.</returns>
        private static DateTime readDateTime(string time)
        {
            DateTime dateTime;
            while (true)
            {
                Console.WriteLine($"Введите время {time} в формате (dd.MM.yyyy hh:mm)");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy hh:mm", null, DateTimeStyles.None, out dateTime))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Неверный формат {time}.");
                }
            }
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
