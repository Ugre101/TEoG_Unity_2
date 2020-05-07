using UnityEngine;

public class PlayerMain : BasicChar
{
    public PlayerMain() : base() => thisPlayer = this;

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
        RaceSystem.AddRace(Races.Humanoid, 100);
        body = new Body(160, 10, 20);
        Currency.Gold += 100;
        InitHealth();
        Essence.Masc.Gain(1000);
        Essence.Femi.Gain(1999);
        Events.SoloEvents.NeedToShit();
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
              //  Debug.Log(new System.Diagnostics.StackFrame(1).GetMethod().DeclaringType + " missed playermain");
            }
            if (thisPlayer == null)
            {
                thisPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
                Debug.LogError("Something tried to call getplayer before player could awake");
            }
            return thisPlayer;
        }
    }
}