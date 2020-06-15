using UnityEngine;

public class PlayerHolder : CharHolder
{
    public static PlayerHolder GetPlayerHolder { get; private set; }

    private void Awake()
    {
        if (GetPlayerHolder == null)
        {
            GetPlayerHolder = this;
        }
        else
        {
            Destroy(gameObject);
        }
        GetTag = gameObject.tag;
    }

    private void Start()
    {
        Setup();
        Player.Essence.Masc.Gain(1000);
        Races test = Races.Bovine;
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(test = UgreTools.CycleThoughEnum<Races>(test));
        }
    }

    public override void Load(string jsonSave)
    {
        JsonUtility.FromJsonOverwrite(jsonSave, Player);
        Unbind();
        Bind();
    }

    public static string GetTag { get; private set; }

    public override BasicChar BasicChar { get => Player; protected set => Player = (PlayerMain)value; }

    public static PlayerMain Player { get; private set; } = new PlayerMain(new Age(), new Body(160, 10, 20), new ExpSystem(1), new Perks());
}