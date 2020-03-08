using UnityEngine;

[System.Serializable]
public class StartRace
{
    [SerializeField] private Races races = Races.Humanoid;
    [SerializeField] private int amount = 100;
    public Races Races => races;
    public int Amount => amount;

    public StartRace(int amount = 100)
    {
        this.amount = amount;
    }
}