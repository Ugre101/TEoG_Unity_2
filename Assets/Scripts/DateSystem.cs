public static class DateSystem
{
    private static int hour = 0;
    private static int day = 1;
    private static int month = 1;

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
        }
    }

    public static int Day
    {
        get => day; private set
        {
            day = value;
            if (day > 29)
            {
                day -= 29;
                Month++;
            }
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

    public static string CurrentDate => $"{Year}-{Month}-{Day} {Hour}:00";

    public static void PassHour(int toPass = 1) => Hour += toPass;

    public static DateSave Save => new DateSave(Year, Month, Day, Hour);

    public static void Load(DateSave toLoad)
    {
        Year = toLoad.year;
        Month = toLoad.month;
        Day = toLoad.day;
        Hour = toLoad.hour;
    }

    public delegate void NewHour();

    public static event NewHour NewHourEvent;
}