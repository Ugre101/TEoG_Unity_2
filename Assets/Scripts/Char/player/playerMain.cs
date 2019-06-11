using UnityEngine;

public class playerMain : BasicChar
{
    public Settings sett;
    public QuestsSystem Quest= new QuestsSystem();
    // Start is called before the first frame update
    private void Start()
    {
        init(1, 100f, 100f);
        strength._baseValue = 10;
        charm._baseValue = 10;
        dexterity._baseValue = 10;
        endurance._baseValue = 10;
        Quest.AddQuest(Quests.Bandit);
        Quest.AddQuest(Quests.Bandit);
        Quest.AddQuest(Quests.Elfs);
        raceSystem.AddRace(Races.Human, 100);

    }
}