using System;
using System.Globalization;
using PersonalMeetingsManager.Utilities;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

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
            MeetingController.Notify += DisplayExitMessage;
            MeetingController.Timeend += DisplayTimer;

            while (true)
            {
                Console.WriteLine("\t\tMeetings Manager");
                Console.WriteLine("\tПеред использованием прочтите справку\n");
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
                try
                {
                    switch (userInput.Key)
                    {
                        case ConsoleKey.N:
                            Console.Clear();
                            var newMeeting = EnterMeeting();
                            meetingController.AddMeeting(newMeeting);
                            break;

                        case ConsoleKey.R:
                            Console.Clear();
                            ShowMeetings(meetingController.Meetings);
                            if (meetingController.Meetings.Count != 0)
                            {
                                var removeIndex = EnterIndex("удалить") - 1;
                                meetingController.RemoveMeeting(removeIndex);
                            }
                            else DisplayExitMessage();
                            break;

                        case ConsoleKey.E:
                            Console.Clear();
                            ShowMeetings(meetingController.Meetings);
                            if (meetingController.Meetings.Count != 0)
                            {
                                var editIndex = EnterIndex("отредактировать") - 1;
                                var changedMeeting = EnterMeeting();
                                meetingController.EditMeeting(editIndex, changedMeeting);
                            }
                            else DisplayExitMessage();
                            break;

                        case ConsoleKey.C:
                            Console.Clear();
                            ShowMeetings(meetingController.Meetings);
                            if (meetingController.Meetings.Count != 0)
                            {
                                var reminderTimeIndex = EnterIndex("изменить время напоминания") - 1;
                                var changedReminderTime = EnterTime();
                                meetingController.EditReminderTime(reminderTimeIndex, changedReminderTime);
                            }
                            else DisplayExitMessage();
                            break;

                        case ConsoleKey.S:
                            Console.Clear();
                            var userIntputDate = EnterDate("для просмотра");
                            ShowMeetings(meetingController.Meetings, userIntputDate);
                            DisplayExitMessage();
                            break;

                        case ConsoleKey.P:
                            Console.Clear();
                            var userIntputDateTxt = EnterDate("для записи в файл");
                            TxtSaver.Save(meetingController.Meetings, userIntputDateTxt);
                            DisplayExitMessage("Экспорт встреч в текстовый файл успешно выполнен.");
                            break;

                        case ConsoleKey.H:
                            Console.Clear();
                            Console.WriteLine("Формат ввода даты и времени: (dd.MM.yyyy HH:mm),");
                            Console.WriteLine("приложение использует 24-х часовой формат времени.");
                            Console.WriteLine("К примеру, дата 8 часов утра 5 минут 12 июля");
                            Console.WriteLine("2012 года будет введена как 12.07.2012 08:05.");
                            Console.WriteLine("Текстовый файл со встречами за определенный день");
                            Console.WriteLine("находится в директории Meetings проекта MyMeetings");
                            DisplayExitMessage();
                            break;

                        case ConsoleKey.Q:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("\nНажата неизвестная клавиша, попробуйте снова.");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                catch (ArgumentNullException ex)
                {
                    DisplayExitMessage($"Ошибка: {ex.Message}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    DisplayExitMessage($"Ошибка: {ex.Message}");
                }
                catch (MeetingCrossingException ex)
                {
                    DisplayExitMessage($"Ошибка: {ex.Message}");
                }
                catch (TimeErrorException ex)
                {
                    DisplayExitMessage($"Ошибка: {ex.Message}");
                }
                Console.Clear();
            }
        }

        /// <summary>
        /// Выводит сообщение об успешном выполнении действии пользоватаеля.
        /// </summary>
        /// <param name="action">Тип действия.</param>
        private static void DisplayExitMessage(string message)
        {
            Console.WriteLine($"\n{message}");
            Console.WriteLine("Нажмите любую клавишу для продолжения.");
            Console.ReadKey();
        }

        /// <summary>
        /// Выводит предложение возврата в меню.
        /// </summary>
        private static void DisplayExitMessage()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения.");
            Console.ReadKey();
        }

        /// <summary>
        /// Выводит предложение напоминание о встрече.
        /// </summary>
        private static void DisplayTimer(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("\nНажмите любую клавишу для продолжения.");
        }

        /// <summary>
        /// Создает новую встречу.
        /// </summary>
        /// <returns>Возвращает новую встречу.</returns>
        private static Meeting EnterMeeting()
        {
            while (true)
            {
                try
                {
                    var startDateTime = EnterDateTime("начала предстоящей встречи");
                    var endDateTime = EnterDateTime("окончания предстоящей встречи");
                    var reminderTime = EnterTime();
                    return new Meeting(startDateTime, endDateTime, reminderTime);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}\n");
                }
                catch (TimeErrorException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}\n");
                }
            }
        }

        /// <summary>
        /// Запрашивает у пользователя индекс.
        /// </summary>
        /// <param name="action">Тип действия.</param>
        /// <returns></returns>
        private static int EnterIndex(string action)
        {
            while (true)
            {
                Console.WriteLine($"Введите номер встречи, которую вы хотели бы {action}:");
                if (int.TryParse(Console.ReadLine(), out int index))
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
        /// <param name="meetingStage">Строка, указывающая на тип вводимого пользователем этапа встречи.</param>
        /// <returns>Возвращает дату и время начала/окончания.</returns>
        private static DateTime EnterDateTime(string meetingStage)
        {
            DateTime dateTime;
            while (true)
            {
                Console.WriteLine($"Введите дату и время {meetingStage} в формате (dd.MM.yyyy HH:mm)");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", null, DateTimeStyles.None, out dateTime))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Неверный формат {meetingStage}.");
                }
            }
            return dateTime;
        }

        /// <summary>
        /// Запрашивает у пользователя дату для <see cref="meetingStage"/>.
        /// </summary>
        /// <param name="meetingStage">Строка, указывающая на тип вводимого пользователем этапа встречи.</param>
        /// <returns>Возвращает введенную пользователем дату.</returns>
        private static DateTime EnterDate(string meetingStage)
        {
            DateTime dateTime;
            while (true)
            {
                Console.WriteLine($"Введите дату {meetingStage} в формате (dd.MM.yyyy)");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, DateTimeStyles.None, out dateTime))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Неверный формат {meetingStage}.");
                }
            }
            return dateTime;
        }

        /// <summary>
        /// Запрашивает у пользователя интервал времени до начала встречи.
        /// </summary>
        /// <returns>Возвращает интервал времени до начала встречи.</returns>
        private static TimeSpan EnterTime()
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
        /// Выводит список всех встреч пользователя <see cref="meetings">.
        /// </summary>
        /// <param name="meetings">Список встреч.</param>
        private static void ShowMeetings(List<Meeting> meetings)
        {
            if (meetings == null)
                throw new ArgumentNullException("Список встреч не может быть null.", nameof(meetings));

            if (meetings.Count == 0)
                Console.WriteLine("Сейчас в Вашем расписании нет ни одной встречи.");
            else
            {
                int counter = 1;
                foreach (Meeting meeting in meetings)
                {
                    Console.WriteLine($"Встреча №{counter}");
                    ShowSingleMeeting(meeting);
                    counter++;
                }
            }
        }

        /// <summary>
        /// Выводит список встреч пользователя на введенную дату <see cref="userDateInput"/>.
        /// </summary>
        /// <param name="meetings">Список встреч.</param>
        /// <param name="userInputDate">Выбранная дата.</param>
        private static void ShowMeetings(List<Meeting> meetings, DateTime userInputDate)
        {
            if (meetings == null)
                throw new ArgumentNullException("Список встреч не может быть null.", nameof(meetings));

            var userOnDateMeetings = from meeting in meetings
                                        where meeting.StartDateTime.Date == userInputDate.Date
                                        select meeting;

            if (meetings.Count == 0)
                Console.WriteLine($"Сейчас в Вашем расписании нет ни одной встречи, запланированной на {userInputDate:D}");
            else
            {
                int counter = 1;
                Console.WriteLine($"Встречи, запланированные на {userInputDate:D}:\n");
                foreach (Meeting meeting in userOnDateMeetings)
                {
                    Console.WriteLine($"Встреча №{counter}");
                    ShowSingleMeeting(meeting);
                    counter++;
                }
            }
        }

        /// <summary>
        /// Выводит данные(время начала/окончания/напоминания) встречи.
        /// </summary>
        /// <param name="meeting">Переданная встреча.</param>
        private static void ShowSingleMeeting(Meeting meeting)
        {
            Console.WriteLine($"Начало:\t\t\t{meeting.StartDateTime:t}");
            Console.WriteLine($"Окончание:\t\t{meeting.EndDateTime:t}");
            Console.WriteLine($"Время напоминания:\t{meeting.ReminderDateTime:t}");
            Console.WriteLine();
        }
    }
}
