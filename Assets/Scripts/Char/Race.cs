using UnityEngine;

public enum Races
{
    Humanoid,
    Human,
    Elf,
    Orc,
    Dwarf,
    Halfling,
    Fairy,
    Incubus,
    Succubus,
    Equine,
    Dragon,
    DragonKin
}

public enum HalfRaces
{
    Centaur,
    HalfElf
}

[System.Serializable]
public class Race
{
    public Race(Races parRace, int parAmount)
    {
        race = parRace;
        essence = parAmount;
    }

    [SerializeField]
    private Races race;

    public Races Name => race;

    [SerializeField]
    private int essence;

    public int Amount => essence;

    public void Gain(int gain)
    {
        essence += Mathf.Max(0, gain);
        DirtyEvent?.Invoke();
    }

    /// <summary>ess -= Abs(lose), returns true if race goes to zero</summary>
    public bool Lose(int lose)
    {
        essence -= Mathf.Abs(lose);
        DirtyEvent?.Invoke();
        return essence <= 0;
    }

    public void LoseAll()
    {
        essence = 0;
        DirtyEvent?.Invoke();
    }

    public delegate void RaceEssenceChange();

    public event RaceEssenceChange DirtyEvent;
}