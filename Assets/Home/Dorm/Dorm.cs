using System.Collections.Generic;
using UnityEngine;

public static class Dorm
{
    public static List<BasicChar> Followers { get; private set; } = new List<BasicChar>();

    public static void AddToDorm(BasicChar basicChar) => Followers.Add(basicChar);

    public static List<DormSave> Save()
    {
        List<DormSave> dormSaves = new List<DormSave>();
        foreach (BasicChar basicChar in Followers)
        {
            DormSave tempDorm = new DormSave(basicChar);
            dormSaves.Add(tempDorm);
        }
        return dormSaves;
    }

    public static void Load(List<DormSave> dormSaves)
    {
        Followers.Clear();
        foreach (DormSave save in dormSaves)
        {
            Followers.Add(save.BasicChar);
        }
    }

    public static bool HasSpace => StartHomeStats.Dorm.Level * 3 > Followers.Count;
}

[System.Serializable]
public struct DormSave
{
    [SerializeField] private string who;

    public string Who => who;
    public BasicChar BasicChar => JsonUtility.FromJson<BasicChar>(who);

    public DormSave(BasicChar Who)
    {
        who = JsonUtility.ToJson(Who);
    }
}