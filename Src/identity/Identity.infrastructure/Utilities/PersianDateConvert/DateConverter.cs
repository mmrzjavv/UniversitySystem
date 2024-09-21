using System;
using System.Globalization;

public static class DateConverter
{
    private static readonly PersianCalendar PersianCalendar = new PersianCalendar();

    // تبدیل تاریخ شمسی به میلادی
    public static DateTime ConvertToGregorian(int year, int month, int day)
    {
        return PersianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
    }

    // تبدیل تاریخ میلادی به شمسی
    public static (int Year, int Month, int Day) ConvertToPersian(DateTime gregorianDate)
    {
        int year = PersianCalendar.GetYear(gregorianDate);
        int month = PersianCalendar.GetMonth(gregorianDate);
        int day = PersianCalendar.GetDayOfMonth(gregorianDate);
        return (year, month, day);
    }

    // تبدیل تاریخ شمسی به میلادی از فرمت string
    public static DateTime ConvertToGregorian(string persianDate)
    {
        var parts = persianDate.Split('/');
        if (parts.Length != 3 || !int.TryParse(parts[0], out var year) ||
            !int.TryParse(parts[1], out var month) ||
            !int.TryParse(parts[2], out var day))
        {
            throw new ArgumentException("Invalid Persian date format. Use YYYY/MM/DD.");
        }
        return ConvertToGregorian(year, month, day);
    }

    // تبدیل تاریخ میلادی به شمسی به فرمت string
    public static string ConvertToPersianString(DateTime gregorianDate)
    {
        var (year, month, day) = ConvertToPersian(gregorianDate);
        return $"{year:000}/{month:00}/{day:00}";
    }

    // اضافه کردن متد برای نمایش تاریخ شمسی به فرمت string
    public static string GetPersianDateString(DateTime gregorianDate)
    {
        return ConvertToPersianString(gregorianDate);
    }
}
