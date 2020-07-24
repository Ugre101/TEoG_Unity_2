using System.Collections;
using UnityEngine;

public static class DateSystem
{
    private static int hour = 0;
    private static int day = 1;
    private static int week = 0;
    private static int month = 1;

    public static int Minute { get; private set; } = 0;
    public static int Year { get; private set; } = 0;

    public static int Month
    {
        get => month;
        private set
        {
            month = value;
            if (month > 12)
            {
                month -= 12;
                Year++;
            }
            NewMonthEvent?.Invoke();
        }
    }

    public static int Week
    {
        get => week; private set
        {
            week = value;
            if (week > 4)
            {
                week -= 4;
                Month++;
            }
            NewWeekEvent?.Invoke();
        }
    }

    public static int Day
    {
        get => day; private set
        {
            day = value;
            if (day > 7)
            {
                day -= 7;
                Week++;
            }
            NewDayEvent?.Invoke();
        }
    }

    public static int Hour
    {
        get => hour;
        private set
        {
            hour = value;
            if (hour > 24)
            {
                hour -= 24;
                Day++;
            }
            NewHourEvent?.Invoke();
        }
    }

    private static void AddMinute(int value)
    {
        Minute += value;
        while (Minute > 60)
        {
            Minute -= 60;
            Hour++;
        }
        NewMinuteEvent?.Invoke(value);
    }

    public static string CurrentDate => $"{Year}-{Month}-{Day} {Hour}:00";

    public static void PassHour(int toPass = 1) => AddMinute(toPass * 60);

    public static void PassMinute(int toPass = 60) => AddMinute(toPass);

    public static IEnumerator TickMinute()
    {
        // Time.time is affected by timescale so no pause check is needed
        float time = Time.time;
        while (true)
        {
            if (time + 1f < Time.time)
            {
                time = Time.time;
                AddMinute(1);
            }
            yield return null;
        }
    }

    public static DateSave Save => new DateSave(Year, Month, Week, Day, Hour);

    public static void Load(DateSave toLoad)
    {
        Year = toLoad.Year;
        Month = toLoad.Month;
        Week = toLoad.Week;
        Day = toLoad.Day;
        Hour = toLoad.Hour;
    }

    public delegate void NewTime();

    public static event NewTime NewHourEvent;

    public static event NewTime NewDayEvent;

    public static event NewTime NewWeekEvent;

    public static event NewTime NewMonthEvent;

    public delegate void NewMinute(int timesCalled);

    public static event NewMinute NewMinuteEvent;
}

public static class DateSystemExtensions
{
    public static int CompareDateHours(this DateSave dateSave)
    {
        int yearsDiff = DateSystem.Year - dateSave.Year;
        int monthDiff = DateSystem.Month - dateSave.Month;
        int weekDiff = DateSystem.Week - dateSave.Week;
        int dayDiff = DateSystem.Day - dateSave.Day;
        int hourDiff = DateSystem.Hour - dateSave.Hour;
        return (yearsDiff * 8064) + (monthDiff * 672) + (weekDiff * 168) + (dayDiff * 24) + hourDiff;
    }

    public static int CompareDateDays(this DateSave dateSave)
    {
        int yearsDiff = DateSystem.Year - dateSave.Year;
        int monthDiff = DateSystem.Month - dateSave.Month;
        int weekDiff = DateSystem.Week - dateSave.Week;
        int dayDiff = DateSystem.Day - dateSave.Day;
        return (yearsDiff * 336) + (monthDiff * 28) + (weekDiff * 7) + dayDiff;
    }

    public static int CompareDateWeeks(this DateSave dateSave)
    {
        int yearsDiff = DateSystem.Year - dateSave.Year;
        int monthDiff = DateSystem.Month - dateSave.Month;
        int weekDiff = DateSystem.Week - dateSave.Week;
        return (yearsDiff * 48) + (monthDiff * 4) + weekDiff;
    }

    public static int CompareDateMonths(this DateSave dateSave)
    {
        int yearsDiff = DateSystem.Year - dateSave.Year;
        int monthDiff = DateSystem.Month - dateSave.Month;
        return (yearsDiff * 12) + monthDiff;
    }

    public static int CompareDateYears(this DateSave dateSave) => DateSystem.Year - dateSave.Year;
}

[System.Serializable]
public struct DateSave
{
    [SerializeField] private int hour, day, week, month, year;

    public int Year => year;
    public int Month => month;
    public int Week => week;
    public int Day => day;
    public int Hour => hour;

    public DateSave(int Year, int Month, int Week, int Day, int Hour)
    {
        this.year = Year;
        this.month = Month;
        this.week = Week;
        this.day = Day;
        this.hour = Hour;
    }
}