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
                Console.WriteLine("R - удалить встречу из списка");    
                Console.WriteLine("E - редактировать встречу из списка");    
                Console.WriteLine("C - изменить напоминание о встрече");    
                Console.WriteLine("S - вывести список встреч на экран");    
                Console.WriteLine("P - экспорт встреч в текстовый файл");    
                Console.WriteLine("H - справка");    
                Console.WriteLine("Q - выход");
                
                var userInput = Console.ReadKey();
                switch(userInput.Key)
                {
                    case ConsoleKey.N:
                        Console.Clear();
                        var newMeeting = EnterMeeting("предстоящей");
                        meetingController.AddMeeting(newMeeting);
                        printExitMessage("Добавление новой встречи");
                        break;
                    case ConsoleKey.R:
                        Console.Clear();
                        var removeIndex = enterIndex("удалить");
                        meetingController.RemoveMeeting(removeIndex);
                        printExitMessage("Удаление встречи");
                        break;
                    case ConsoleKey.E:
                        // TODO: добавить проверку на наличие встреч в списке.
                        Console.Clear();
                        var editIndex = enterIndex("отредактировать");
                        var changedMeeting = EnterMeeting("");
                        meetingController.EditMeeting(editIndex, changedMeeting);
                        printExitMessage("Редактирование встречи");
                        break;
                    case ConsoleKey.C:
                        // TODO: исправить -> не изменяет дату и вермя напоминания.
                        Console.Clear();
                        var changeReminderIndex = enterIndex("изменить время напоминания");
                        var changedReminderTime = enterDateTime("напоминания о встрече");
                        meetingController.ChangeReminderDateTime(changeReminderIndex, changedReminderTime);
                        printExitMessage("Измнение времени напоминания");
                        break;
                    case ConsoleKey.S:
                        Console.Clear();
                        // TODO: изменить способ вывода списка встреч в консоль -> добавить к номеру встречи её дату.
                        showMeetings(meetingController.Meetings);
                        printExitMessage("Вывод списка на экран");
                        break;
                    case ConsoleKey.P:
                        Console.Clear();
                        // TODO: изменить способ вывода списка встреч в консоль -> вывод встреч на определеннуюю дату, добавить к имени файла дату встречи, 
                        //       добавить в самое начало текстового файла дату.
                        TxtSaver.Save(meetingController.Meetings);
                        printExitMessage("Экспорт встреч в текстовый файл");
                        break;
                    case ConsoleKey.H:
                        Console.Clear();

                }
            }
        }
        //12.12.2012 12:50

        private static void printExitMessage(string action)
        {
            Console.WriteLine($"{action} успешно выполнено.");
            Console.WriteLine("Нажмите любую кнопку для выхода.");
            Console.ReadKey();
        }

        /// <summary>
        /// Создает новую встречу.
        /// </summary>
        /// <returns>Возвращает новую встречу.</returns>
        private static Meeting EnterMeeting(string value)
        {
            var startDateTime = enterDateTime($"начала {value} встречи");
            var endDateTime = enterDateTime($"окончания {value} встречи");
            var reminderDateTime = enterDateTime($"напоминания о {value} встрече");
            return new Meeting(startDateTime, endDateTime, reminderDateTime);
        }

        private static int enterIndex(string value)
        {
            int index;
            while (true)
            {
                Console.WriteLine($"Введите номер встречи, которую вы хотели бы {value}:");
                if (int.TryParse(Console.ReadLine(), out index))
                {
                    return index;
                }
                else
                {
                    Console.WriteLine("Неправильно введен номер встречи.");
                }
            }
        }

        /// <summary>
        /// Запрашивает у пользователя дату и время.
        /// </summary>
        /// <param name="time">Строка, указывающая на тип вводимого пользователем этапа встречи.</param>
        /// <returns>Возвращает дату и время начала/окончания/напоминания встречи.</returns>
        private static DateTime enterDateTime(string time)
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
