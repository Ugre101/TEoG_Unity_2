using UnityEngine;

public class playerMain : BasicChar
{
    public Settings sett;
    public QuestsSystem Quest= new QuestsSystem();
    // Start is called before the first frame update
    private void Start()
    {
        init(1, 100f, 100f);
        Str = 10;
        Charm = 10;
        Dex = 10;
        End = 10;
        Str++;
        Femi.Gain(200f);
        Masc.Gain(2000f);
        AddDick();
        Debug.Log(sett.DicksLook(Dicks));
        Quest.AddQuest(Quests.Bandit);
    }
}