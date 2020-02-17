using UnityEngine;

public class PlayerMain : BasicChar
{
    public PlayerMain() : base()
    {
        thisPlayer = this;
    }

    // public Settings sett;
    public override void Awake()
    {
        if (thisPlayer == null)
        {
            thisPlayer = this;
        }
        else if (thisPlayer != this)
        {
            Destroy(gameObject);
        }
        base.Awake();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        RaceSystem.AddRace(Races.Human, 100);
        body = new Body(160, 10, 20);
        Inventory.AddItem(ItemId.Pouch);
        for (int i = 0; i < 40; i++)
        {
            Inventory.AddItem(ItemId.Stick);
        }
        Essence.StableEssence.BaseValue += 100;
        Essence.Masc.Gain(99);
        Essence.Femi.Gain(99);
        Currency.Gold += 100;
        InitHealth();
    }

    public void PlayerInit(string first, string last)
    {
        Identity.FirstName = first;
        Identity.LastName = last;
    }

    private static PlayerMain thisPlayer;
    private static string thisTag;

    public static string GetTag => thisTag = thisTag ?? GetPlayer.tag;

    public static PlayerMain GetPlayer
    {
        get
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log(new System.Diagnostics.StackFrame(1).GetMethod().DeclaringType + " missed playermain");
            }
            return thisPlayer = thisPlayer != null ? thisPlayer : GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
        }
    }
}