﻿using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace PersonalMeetingsManager.Utilities
{
    public static class TxtSaver
    {
        /// <summary>
        /// Сохраняет список всех встреч <see cref="meetings"> в текстовый файл.
        /// </summary>
        /// <param name="meetings">Список встреч.</param>
        public static void Save(List<Meeting> meetings, DateTime userInputDate)
        {
            if (meetings == null)
                throw new ArgumentNullException("Передан пустой список", nameof(meetings));

            var userOnDateMeetings = (from meeting in meetings
                                      where meeting.StartDateTime.Date == userInputDate.Date
                                      select meeting).ToList();

            var pathString = "MyMeetings";
            var fileName = "MyMeetings.txt";
    
            if (!Directory.Exists(pathString))
                Directory.CreateDirectory(pathString);

            pathString = Path.Combine(pathString, fileName);
            FileStream fs = null;
            try
            {
                fs = new FileStream(pathString, FileMode.Create);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    if (userOnDateMeetings.Count == 0)
                        sw.WriteLine($"Сейчас в Вашем расписании нет ни одной встречи, запланированной на {userInputDate.ToString("D")}");
                    else
                    {
                        int counter = 1;
                        sw.WriteLine($"Встречи, запланированные на {userInputDate.ToString("D")}:\n");
                        foreach (Meeting meeting in userOnDateMeetings)
                        {
                            sw.WriteLine($"Встреча №{counter}");
                            sw.WriteLine($"Начало:\t\t\t{meeting.StartDateTime.ToString("t")}");
                            sw.WriteLine($"Окончание:\t\t{meeting.EndDateTime.ToString("t")}");
                            sw.WriteLine($"Время напоминания:\t{meeting.ReminderDateTime.ToString("t")}");
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
