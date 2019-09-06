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
        strength._baseValue = 10;
        charm._baseValue = 10;
        dexterity._baseValue = 10;
        endurance._baseValue = 10;
        Quest.AddQuest(Quests.Bandit);
        Quest.AddQuest(Quests.Bandit);
        Quest.AddQuest(Quests.Elfs);
        raceSystem.AddRace(Races.Human, 100);
        firstName = "adofa";
        Body = new Body(160, 60, 10, 20);
        Inventory.AddItem(ItemRefs.Item);
        Inventory.AddItem(ItemRefs.TestPotion);
        // Inventory.AddItem(new TestPotion());
        // Inventory.AddItem(new TestPotion());
    }
}