using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save 
{
    private playerMain Player;
    private Transform Pos;
    private string playerFlag = "Player";
    private string posFlag = "Pos";
    private string json;
    public Save (playerMain player, Transform pos)
    {
        Player = player;
        Pos = pos.transform;
    }
    public string SaveData()
    {
        json = "Player";
        json += JsonUtility.ToJson(Player);
        json += "Pos";
        json += JsonUtility.ToJson(Pos.position);
        return json;
    }
    public void LoadData(string json)
    {
        int playerFlagIndex = json.IndexOf(playerFlag);
        int posFlagIndex = json.IndexOf(posFlag);
        string playerPart = json.Substring(playerFlagIndex + playerFlag.Length, posFlagIndex - playerFlag.Length);
        string posPary = json.Substring(posFlagIndex + posFlag.Length, json.Length - (posFlagIndex + posFlag.Length));
        Vector3 pspspsp = JsonUtility.FromJson<Vector3>(posPary);
        Pos.position = pspspsp;
        JsonUtility.FromJsonOverwrite(playerPart, Player);
    }
}
