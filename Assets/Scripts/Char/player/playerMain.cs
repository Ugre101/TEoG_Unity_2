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
        Init(1, 100, 100);
        RaceSystem.AddRace(Races.Human, 100);
        body = new Body(160, 10, 20);
        Inventory.AddItem(ItemId.Pouch);
        for (int i = 0; i < 40; i++)
        {
            Inventory.AddItem(ItemId.Stick);
        }
        SexualOrgans.Balls.AddBalls(500);
        SexualOrgans.Boobs.AddBoobs();
        SexualOrgans.Dicks.AddDick(12);
        SexualOrgans.Vaginas.AddVag(7);
        Currency.Gold += 100;
        base.Start();
    }

    public void PlayerInit(string first, string last)
    {
        firstName = first;
        lastName = last;
        Debug.Log(firstName);
    }
}