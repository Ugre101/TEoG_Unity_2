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

    public static bool HasSpace => StartHomeStats.Dorm.Level * 3 > Followers.Count;
}

[System.Serializable]
public struct DormSave
{
    [SerializeField] private string who;

    public BasicChar BasicChar => JsonUtility.FromJson<BasicChar>(who);

    public DormSave(BasicChar Who) => who = JsonUtility.ToJson(Who);
}