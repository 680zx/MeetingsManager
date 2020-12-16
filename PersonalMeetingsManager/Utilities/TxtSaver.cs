using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalMeetingsManager.Utilities
{
    public static class TxtSaver
    {
        public static void Save(List<Meeting> items)
        {
            var fileName = "MyMeetings.txt";

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
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
