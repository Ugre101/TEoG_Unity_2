public class DisplayMod
{
    public DisplayMod(string parSource, int parDuration)
    {
        Source = parSource;
        Duration = parDuration;
    }

    public string Source { get; private set; }
    public int Duration { get; private set; }
}