using System.Collections.Generic;
using System;

namespace PersonalMeetingsManager.Utilities
{
    public static class TxtSaver
    {
        /// <summary>
        /// Сохраняет список всех встреч <see cref="items"> в текстовый файл.
        /// </summary>
        /// <param name="items">Список встреч.</param>
        public static void Save(List<Meeting> items)
        {
            if (items == null)
                throw new ArgumentNullException("Передан пустой список", nameof(items));

            var pathString = "MyMeetings";
            var fileName = "MyMeetings.txt";

            if (!System.IO.Directory.Exists(pathString))
                System.IO.Directory.CreateDirectory(pathString);

            pathString = System.IO.Path.Combine(pathString, fileName);

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(pathString))
            {
                if (items.Count == 0)
                {
                    sw.WriteLine("Сейчас в Вашем расписании нет ни одной встречи.");
                }
                else
                {
                    int counter = 1;
                    foreach (Meeting item in items)
                    {
                        sw.WriteLine($"Встреча №{counter}");
                        sw.WriteLine($"Начало:\t\t\t{item.StartDateTime}");
                        sw.WriteLine($"Окончание:\t\t{item.EndDateTime}");
                        sw.WriteLine($"Время напоминания:\t{item.ReminderDateTime}");
                        sw.WriteLine();
                        counter++;
                    }
                }
            }
        }
    }
}
