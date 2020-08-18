using System.Collections.Generic;
using UnityEngine;

public static class Dorm
{
    public static Dictionary<string, BasicChar> Followers { get; private set; } = new Dictionary<string, BasicChar>();

    public static void AddToDorm(BasicChar basicChar) => Followers.Add(basicChar.Identity.Id, basicChar);

    public static List<DormSave> Save()
    {
        List<DormSave> dormSaves = new List<DormSave>();
        foreach (BasicChar basicChar in Followers.Values)
        {
            dormSaves.Add(new DormSave(basicChar));
        }
        return dormSaves;
    }

    public static void Load(List<DormSave> dormSaves)
    {
        Followers.Clear();
        foreach (DormSave save in dormSaves)
        {
            Followers.Add(save.BasicChar.Identity.Id, save.BasicChar);
        }
    }

    public static bool HasSpace => DormUpgrades.Dorm.Level * 3 > Followers.Count;
}

[System.Serializable]
public struct DormSave
{
    [SerializeField] private string who;

    public BasicChar BasicChar => JsonUtility.FromJson<BasicChar>(who);

    public DormSave(BasicChar Who) => who = JsonUtility.ToJson(Who);
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

    private void EveryHour() => dormAI.HourlyTick(this);
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