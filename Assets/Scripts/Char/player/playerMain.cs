using UnityEngine;

public class playerMain : BasicChar
{
    // public Settings sett;
    [Space]
    public QuestsSystem Quest = new QuestsSystem();

    public PlayerFlags PlayerFlags = new PlayerFlags();

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Init(1, 100f, 100f);
        Quest.AddQuest(Quests.Bandit);
        Quest.AddQuest(Quests.Bandit);
        Quest.AddQuest(Quests.Elfs);
        raceSystem.AddRace(Races.Human, 100);
        Body = new Body(160, 10, 20);
        // Inventory.AddItem(ItemId.Potion);
        Inventory.AddItem(ItemId.Pouch);
        // Inventory.AddItem(new TestPotion());
        // Inventory.AddItem(new TestPotion());
    }

    public void PlayerInit(string first, string last)
    {
        firstName = first;
        lastName = last;
        Debug.Log(firstName);
    }
}