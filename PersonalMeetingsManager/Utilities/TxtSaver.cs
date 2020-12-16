using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace PersonalMeetingsManager.Utilities
{
    public static class TxtSaver
    {
        /// <summary>
        /// Сохраняет список всех встреч <see cref="items"> в текстовый файл.
        /// </summary>
        /// <param name="items">Список встреч.</param>
        public static void Save(List<Meeting> items, DateTime userDate)
        {
            if (items == null)
                throw new ArgumentNullException("Передан пустой список", nameof(items));

            var pathString = "MyMeetings";
            var fileName = "MyMeetings.txt";

            if (!Directory.Exists(pathString))
                Directory.CreateDirectory(pathString);

            pathString = Path.Combine(pathString, fileName);

            var userDateTimeMeetings = from item in items
                                       where item.StartDateTime.Date == userDate.Date
                                       select item;

            using (StreamWriter sw = new StreamWriter(pathString))
            {
                if (items.Count == 0)
                    sw.WriteLine($"Сейчас в Вашем расписании нет ни одной встречи, запланированной на {userDate.ToString("D")}");
                else
                {
                    int counter = 1;
                    sw.WriteLine($"Встречи, запланированные на {userDate.ToString("D")}");
                    foreach (Meeting item in userDateTimeMeetings)
                    {
                        sw.WriteLine($"Встреча №{counter}");
                        sw.WriteLine($"Начало:\t\t\t{item.StartDateTime.ToString("t")}");
                        sw.WriteLine($"Окончание:\t\t{item.EndDateTime.ToString("t")}");
                        sw.WriteLine($"Время напоминания:\t{item.ReminderDateTime.ToString("t")}");
                        sw.WriteLine();
                        counter++;
                    }
                }
            }
        }
    }
}
