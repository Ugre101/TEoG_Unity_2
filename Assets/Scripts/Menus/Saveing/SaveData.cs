using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Save 
{
    private playerMain Player;
    private Transform Pos;
    private Dorm dorm;
    private string playerFlag = "PlayerPart";
    private string posFlag = "PosPart";
    private string json;
    private string dormNames;
    private string dormChars;
    private PlayerSave save;
    public Save (playerMain player, Transform pos, Dorm theDorm)
    {
        Player = player;
        Pos = pos.transform;
        dorm = theDorm;
    }
    public string SaveData()
    {
        save = new PlayerSave(Pos.position, Player);
        List<DormSave> temp = new List<DormSave>();
        foreach(Transform child in dorm.transform)
        {
            DormSave tempDorm = new DormSave(child.name, child.GetComponent<BasicChar>());
            temp.Add(tempDorm);
        }
        FullSave fullSave = new FullSave(save, temp);
        Debug.Log(JsonUtility.ToJson(fullSave));
        return JsonUtility.ToJson(fullSave);
    }
    public void LoadData(string json)
    {
        FullSave fullSave = JsonUtility.FromJson<FullSave>(json);
        Pos.position = fullSave.playerPart.pos;
        JsonUtility.FromJsonOverwrite(fullSave.playerPart.who, Player);
        foreach(Transform child in dorm.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (DormSave dormSave in fullSave.dormPart)
        {
            dorm.Load(dormSave);
        }
    }
}
[Serializable]
public class FullSave
{
    public PlayerSave playerPart;
    public List <DormSave> dormPart;
    public FullSave(PlayerSave player,List <DormSave> dorm)
    {
        playerPart = player;
        dormPart = dorm;
    }
}
[Serializable]
public class PlayerSave
{
    public Vector3 pos;
    public string who;
    public PlayerSave(Vector3 Pos, BasicChar whom)
    {
        pos = Pos;
        who = JsonUtility.ToJson(whom);
    } 
}

[Serializable]
public class DormSave
{
    public string name;
    public string who;
    public DormSave(string Name, BasicChar Who)
    {
        name = Name;
        who = JsonUtility.ToJson(Who);
    }

}

