using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Dorm
{
    public static Dictionary<string, DormMate> Followers { get; private set; } = new Dictionary<string, DormMate>();

    public static void AddToDorm(BasicChar basicChar)
    {
        DormMate newMate = new DormMate(basicChar);
        newMate.BindToDateSystem();

        Followers.Add(basicChar.Identity.Id, newMate);
    }

    public static List<DormSave> Save() => Followers.Values.Select(dormMate => new DormSave(dormMate)).ToList();

    public static void Load(IEnumerable<DormSave> dormSaves)
    {
        foreach (DormMate dormMate in Followers.Values)
        {
            dormMate.UnBindToDateSystem();
        }
        Followers.Clear();

        foreach (DormSave save in dormSaves)
        {
            Followers.Add(save.DormMate.BasicChar.Identity.Id, save.DormMate);
            save.DormMate.BindToDateSystem();
        }
    }

    public static bool HasSpace => DormUpgrades.Dorm.Level * 3 > Followers.Count;
}

[System.Serializable]
public struct DormSave
{
    [SerializeField] private string who;

    public DormMate DormMate => GameManager.LoadFromGameVersion <= 0.043f
                ? new DormMate(JsonUtility.FromJson<BasicChar>(who))
                : JsonUtility.FromJson<DormMate>(who);

    public DormSave(DormMate who) => this.who = JsonUtility.ToJson(who);
}

[System.Serializable]
public class DormMate
{
    [SerializeField] private BasicChar basicChar;
    [SerializeField] private DormAI dormAI;
    [SerializeField] private int morale;

    public DormMate(BasicChar basicChar)
    {
        this.basicChar = basicChar;
        dormAI = new DormAI();
        morale = 100;
    }

    public int Morale => morale;

    public BasicChar BasicChar => basicChar;

    public void IncreaseMorale(int value) => morale += Mathf.Abs(value);

    public void DecreaseMorale(int value) => morale -= Mathf.Abs(value);

    private void EveryHour()
    {
        dormAI.HourlyTick(this);
        basicChar.DoEveryHour();
    }

    private void EveryDay()
    {
        basicChar.DoEveryDay();
    }

    public void BindToDateSystem()
    {
        DateSystem.NewHourEvent += EveryHour;
        DateSystem.NewDayEvent += EveryDay;
    }

    public void UnBindToDateSystem()
    {
        DateSystem.NewHourEvent -= EveryHour;
        DateSystem.NewDayEvent -= EveryDay;
    }
}

[System.Serializable]
public class DormAI
{
    [SerializeField] private bool awake = true, dayPerson = true;
    [SerializeField] private int beenAwake = 0, allowedSleepTime = 8, ruledAwakeTime = 16;

    public int GetBiologicalAwakeTime(DormMate dormMate)
    {
        // Maybe add race spefic awake time.

        // Add Awake bonuses like coffe.
        return 16 + dormMate.BasicChar.RaceSystem.CurrentRace().AwakeTimeModifer();
    }

    public void HourlyTick(DormMate dormMate)
    {
        if (awake)
        {
            beenAwake++;
        }
        else
        {
            // Add bonuses for room upgrades
            dormMate.IncreaseMorale(1);
            beenAwake -= 2;
        }

        if (!awake || beenAwake < GetBiologicalAwakeTime(dormMate)) return;

        if (beenAwake >= ruledAwakeTime)
        {
            awake = false;
        }
        else
        {
            // Try to stay awake
            dormMate.DecreaseMorale(1);
            float chanceToStayAwake = 1f / 1f + (beenAwake - GetBiologicalAwakeTime(dormMate));
            if (Random.value < chanceToStayAwake)
            {
                // Stays awake
            }
            else
            {
                awake = false;
            }
        }
    }
}

public static class DormRules
{
}