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
        thisTag = gameObject.tag;
    }

    private void Start()
    {
        if (!isSetuped)
        {
            Setup();
            isSetuped = true;
        }
    }

    public override void Setup()
    {
        BasicChar.Setup();
    }

    private void OnDestroy()
    {
        BasicChar.BeforeDestroy();
    }

    public override void Load(string jsonSave) => Player = JsonUtility.FromJson<PlayerMain>(jsonSave);

    private static string thisTag;
    public static string GetTag => thisTag;
    private static PlayerMain player = new PlayerMain(new Age(), new Body(160, 10, 20), new ExpSystem(1), new Perks(), new EssenceSystem(new Essence(), new Essence(), new CharStats(0)));
    public override BasicChar BasicChar { get => player; protected set => Player = (PlayerMain)value; }
    private static bool isSetuped = false;

    public static PlayerMain Player
    {
        get
        {
            if (!isSetuped)
            {
                player.Setup();
            }
            return player;
        }
        private set => player = value;
    }
}