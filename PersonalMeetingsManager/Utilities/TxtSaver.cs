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
        public static void Save(List<Meeting> items, DateTime userInputDate)
        {
            if (items == null)
                throw new ArgumentNullException("Передан пустой список", nameof(items));

            var userDateTimeMeetings = from item in items
                                       where item.StartDateTime.Date == userInputDate.Date
                                       select item;

            var pathString = "MyMeetings";
            var fileName = "MyMeetings.txt";
    
            if (!Directory.Exists(pathString))
                Directory.CreateDirectory(pathString);

            pathString = Path.Combine(pathString, fileName);
            FileStream fs = null;
            try
            {
                fs = new FileStream(pathString, FileMode.OpenOrCreate);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    if (items.Count == 0)
                        sw.WriteLine($"Сейчас в Вашем расписании нет ни одной встречи, запланированной на {userInputDate.ToString("D")}");
                    else
                    {
                        int counter = 1;
                        sw.WriteLine($"Встречи, запланированные на {userInputDate.ToString("D")}");
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
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
            
        }
    }
}
