using UnityEngine;

public class PlayerMain : BasicChar
{
    // public Settings sett;
    [Space]
    [SerializeField]
    private QuestsSystem quest = new QuestsSystem();

    [SerializeField]
    private PlayerFlags playerFlags = new PlayerFlags();

    public QuestsSystem Quest => quest;
    public PlayerFlags PlayerFlags => playerFlags;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Init(1, 100, 100);
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