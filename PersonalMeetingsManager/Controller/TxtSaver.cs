using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PersonalMeetingsManager.Controller
{
    public static class TxtSaver
    {
        public static void Save<T>(List<T> items)
        {
            var fileName = "MyMeetings.txt";

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (items.Count == 0)
                {
                    AddText(fs, "Сейчас в Вашем расписании нет ни одной встречи.");
                    fs.Close();
                }
                else
                {
                    int counter = 1;
                    foreach (T item in items)
                    {
                        Console.WriteLine($"Встреча №{counter}");
                        Console.WriteLine($"Начало:\t\t\t{item.StartDateTime}");
                        Console.WriteLine($"Окончание:\t\t{meeting.EndDateTime}");
                        Console.WriteLine($"Время напоминания:\t{meeting.ReminderDateTime}");
                        Console.WriteLine();
                        counter++;
                    }
                }
            }
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
