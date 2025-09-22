using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== УНИВЕРСАЛЬНЫЙ КАЛЕНДАРЬ ===");
        
        int year = GetValidInput("Введите год: ", 1, 9999);
        int month = GetValidInput("Введите месяц (1-12): ", 1, 12);
        int startDayOfWeek = GetValidInput("Введите номер дня недели начала месяца (1-пн., 7-вс): ", 1, 7);
        int dayToCheck = GetValidInput("Введите день месяца: ", 1, DateTime.DaysInMonth(year, month));
        
        bool isHoliday = IsHoliday(year, month, dayToCheck, startDayOfWeek);
        
        Console.WriteLine("\n--- Проверяем выходной ли день ---");
        Console.WriteLine(isHoliday ? "Выходной день" : "Рабочий день");
        
        ShowCalendar(year, month, startDayOfWeek, dayToCheck);
    }
    
    static int GetValidInput(string prompt, int min, int max)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                return value;
            Console.WriteLine($"Ошибка! Введите число от {min} до {max}");
        }
    }
    
    static bool IsHoliday(int year, int month, int day, int startDayOfWeek)
    {
        int dayOfWeek = (startDayOfWeek + day - 2) % 7 + 1;
        if (dayOfWeek == 6 || dayOfWeek == 7) return true;
        
        var holidays = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 } },
            { 2, new List<int> { 23 } },
            { 3, new List<int> { 8 } },
            { 5, new List<int> { 1, 9 } },
            { 6, new List<int> { 12 } },
            { 11, new List<int> { 4 } }
        };
        
        return holidays.ContainsKey(month) && holidays[month].Contains(day);
    }
    
    static void ShowCalendar(int year, int month, int startDayOfWeek, int highlightedDay = 0)
    {
        Console.WriteLine($"\n=== Календарь {GetMonthName(month)} {year} года ===");
        string[] daysOfWeek = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
        foreach (string dayName in daysOfWeek) Console.Write($"{dayName}\t");
        Console.WriteLine();
        
        int daysInMonth = DateTime.DaysInMonth(year, month);
        int currentDayOfWeek = startDayOfWeek;
        
        for (int i = 1; i < startDayOfWeek; i++) Console.Write("\t");
        for (int day = 1; day <= daysInMonth; day++)
        {
            bool isHoliday = IsHoliday(year, month, day, startDayOfWeek);
            bool isHighlighted = (day == highlightedDay);
            
            if (isHighlighted) Console.Write($"[{day}]\t");
            else if (isHoliday) Console.Write($"{day}*\t");
            else Console.Write($"{day}\t");
            
            if (currentDayOfWeek == 7) { Console.WriteLine(); currentDayOfWeek = 1; }
            else currentDayOfWeek++;
        }
        Console.WriteLine("\n* - выходной день");
    }
    
    static string GetMonthName(int month)
    {
        string[] monthNames = {"Январь","Февраль","Март","Апрель","Май","Июнь","Июль","Август","Сентябрь","Октябрь","Ноябрь","Декабрь"};
        return monthNames[month - 1];
    }
}
