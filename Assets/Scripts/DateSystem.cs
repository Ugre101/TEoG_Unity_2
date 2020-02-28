﻿using System.Collections;
using UnityEngine;

public static class DateSystem
{
    private static int hour = 0;
    private static int day = 1;
    private static int week = 0;
    private static int month = 1;
    private static int minute = 0;
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

    public static int Minute
    {
        get => minute;
        private set
        {
            minute = value;
            if (minute > 60)
            {
                minute -= 60;
                Hour++;
            }
            NewMinuteEvent?.Invoke();
        }
    }

    public static string CurrentDate => $"{Year}-{Month}-{Day} {Hour}:00";

    public static void PassHour(int toPass = 1)
    {
        for (int h = 0; h < toPass * 60; h++) Minute++;
    }

    public static void PassMinute(int toPass = 60)
    {
        for (int m = 0; m < toPass; m++) Minute++;
    }

    public static IEnumerator TickMinute()
    {
        // Time.time is affected by timescale so no pause check is needed
        float time = Time.time;
        while (true)
        {
            if (time + 1f < Time.time)
            {
                time = Time.time;
                Minute++;
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

    public delegate void NewHour();

    public static event NewHour NewHourEvent;

    public delegate void NewDay();

    public static event NewDay NewDayEvent;

    public delegate void NewWeek();

    public static event NewWeek NewWeekEvent;

    public delegate void NewMonth();

    public static event NewMonth NewMonthEvent;

    public delegate void NewMinute();

    public static event NewMinute NewMinuteEvent;
}

[System.Serializable]
public struct DateSave
{
    [SerializeField] private int hour;
    [SerializeField] private int year;
    [SerializeField] private int month;
    [SerializeField] private int week;
    [SerializeField] private int day;

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