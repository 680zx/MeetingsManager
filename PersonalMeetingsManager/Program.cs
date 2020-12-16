using System;
using System.Globalization;
using PersonalMeetingsManager.Utilities;
using System.Collections.Generic;
using System.Threading;

namespace PersonalMeetingsManager
{
    class Program
    {
        /// <summary>
        /// Интерфейс и главный цикл программы.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            MeetingController meetingController = new MeetingController();
            
            while(true)
            {
                Console.WriteLine("\tMeetings Manager\n");
                Console.WriteLine("Выберите действие:");
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
                        var newMeeting = enterMeeting("предстоящей");
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
                        var changedMeeting = enterMeeting("");
                        meetingController.EditMeeting(editIndex, changedMeeting);
                        printExitMessage("Редактирование встречи");
                        break;
                    case ConsoleKey.C:
                        // TODO: исправить -> не изменяет дату и вермя напоминания.
                        Console.Clear();
                        var changeReminderIndex = enterIndex("изменить время напоминания");
                        var changedReminderTime = enterTime();
                        meetingController.ChangeReminderTime(changeReminderIndex, changedReminderTime);
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
                        Console.WriteLine("Формат ввода даты и времени: (dd.MM.yyyy HH:mm),");
                        Console.WriteLine("приложение использует 24-х часовой формат времени.");
                        Console.WriteLine("К примеру, дата 8 часов утра 5 минут 12 июля");
                        Console.WriteLine("2012 года будет введена как 12.07.2012 08:05.");
                        Console.WriteLine("Текстовый файл со встречами за опреденный день");
                        Console.WriteLine("находится в директории Meetings проекта MyMeetings");
                        printExitMessage("Вывод справки");
                        break;
                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nНажата неизвестная клавиша, попробуйте снова."); 
                        Thread.Sleep(1000);
                        break;
                }
                Console.Clear();
            }
        }
        //01.01.2021 23:50

        /// <summary>
        /// Выводит сообщение об успешном выполнении действии пользоватаеля.
        /// </summary>
        /// <param name="action">Тип действия.</param>
        private static void printExitMessage(string action)
        {
            Console.WriteLine($"\n{action} успешно выполнено.");
            Console.WriteLine("Нажмите любую клавишу для возврата в главное меню.");
            Console.ReadKey();
        }

        /// <summary>
        /// Создает новую встречу.
        /// </summary>
        /// <returns>Возвращает новую встречу.</returns>
        private static Meeting enterMeeting(string value)
        {
            while(true)
                try
                {
                    var startDateTime = enterDateTime($"начала {value} встречи");
                    var endDateTime = enterDateTime($"окончания {value} встречи");
                    var reminderTime = startDateTime.Subtract(enterTime());
                    return new Meeting(startDateTime, endDateTime, reminderTime);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.ParamName}\n");
                }
           
        }

        /// <summary>
        /// Запращивает у пользователя индекс.
        /// </summary>
        /// <param name="action">Тип действия.</param>
        /// <returns></returns>
        private static int enterIndex(string action)
        {
            int index;
            while (true)
            {
                Console.WriteLine($"Введите номер встречи, которую вы хотели бы {action}:");
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
                Console.WriteLine($"Введите дату и время {time} в формате (dd.MM.yyyy HH:mm)");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", null, DateTimeStyles.None, out dateTime))
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
        /// Запрашивает у пользователя интервал времени.
        /// </summary>
        /// <returns>Возвращает интервал времени до начала встречи.</returns>
        private static TimeSpan enterTime()
        {
            TimeSpan timeSpan;
            while (true)
            {
                Console.WriteLine($"Введите время, за которое нужно Вам напомнить о встрече (HH:mm):");
                if (TimeSpan.TryParseExact(Console.ReadLine(), "g", null, out timeSpan))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Неверный формат времени.");
                }
            }
            return timeSpan;
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
                    Console.WriteLine($"Время напоминания:\t{meeting.ReminderTime}");
                    Console.WriteLine();
                    counter++;
                }
            }
        }
    }
}
