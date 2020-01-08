public static class Looks
{
    public static string Height(this BasicChar who) => Settings.MorInch(who.Body.Height.Value);

    public static string Weight(this BasicChar who) => Settings.KgorP(who.Body.Weight);

    public static string Summary(this BasicChar who)
    {
        string title = who.Identity.FullName;
        string desc = $"A {who.Height()} tall {who.Race} {who.Gender.ToString()}.";
        string stats = $"{who.Age.AgeYears}years old\nWeight: {Weight(who)}\nHeight: {Height(who)}";
        return $"{title}\n{desc}\n{stats}";
    }
}