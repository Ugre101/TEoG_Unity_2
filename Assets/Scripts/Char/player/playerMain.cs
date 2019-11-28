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
        RaceSystem.AddRace(Races.Human, 100);
        body = new Body(160, 10, 20);
        Inventory.AddItem(ItemId.Pouch);
        for (int i = 0; i < 40; i++)
        {
            Inventory.AddItem(ItemId.Stick);
        }
    }

    public void PlayerInit(string first, string last)
    {
        firstName = first;
        lastName = last;
        Debug.Log(firstName);
    }
}