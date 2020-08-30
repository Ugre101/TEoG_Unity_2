using System.Collections.Generic;
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

    public static List<DormSave> Save()
    {
        List<DormSave> dormSaves = new List<DormSave>();
        foreach (DormMate basicChar in Followers.Values)
        {
            dormSaves.Add(new DormSave(basicChar));
        }
        return dormSaves;
    }

    public static void Load(List<DormSave> dormSaves)
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

    public DormSave(DormMate Who) => who = JsonUtility.ToJson(Who);
}

public class DormMate
{
    [SerializeField] private BasicChar basicChar;
    [SerializeField] private DormAI dormAI;
    [SerializeField] private int morale = 100;

    public DormMate(BasicChar basicChar)
    {
        this.BasicChar = basicChar;
        this.dormAI = new DormAI();
        DateSystem.NewHourEvent += EveryHour;
    }

    public int Morale => morale;

    public BasicChar BasicChar { get => basicChar; private set => basicChar = value; }

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
    private bool awake = true, dayPerson = true;
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

        if (awake && beenAwake >= GetBiologicalAwakeTime(dormMate))
        {
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
}

public static class DormRules
{
}