﻿using System.Collections.Generic;
using System;
using System.IO;

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

            if (!Directory.Exists(pathString))
                Directory.CreateDirectory(pathString);

            pathString = Path.Combine(pathString, fileName);

            using (StreamWriter sw = new StreamWriter(pathString))
            {
                if (items.Count == 0)
                {
                    sw.WriteLine("Сейчас в Вашем расписании нет ни одной встречи.");
                }
                else
                {
                    int counter = 1;
                    sw.WriteLine($"Ваши встречи на {items[0].StartDateTime.ToString("D")}\n");
                    foreach (Meeting item in items)
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
