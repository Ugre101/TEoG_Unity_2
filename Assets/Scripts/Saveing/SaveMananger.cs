using System.IO;
using UnityEngine;
using System;

public class SaveMananger : MonoBehaviour
{
    public static SaveMananger instance;
    public playerMain player;

    private string _mainPath;

    private void Start()
    {
       _mainPath = Application.persistentDataPath + "/Game_Save/"; 
       if (!Directory.Exists(_mainPath))
        {
            Directory.CreateDirectory(_mainPath);
        }
    }
    public void NewSaveGame()
    {
        DateTime now = DateTime.Now;
        string cleanPath = "playerName" + now;
        char[] illegal = Path.GetInvalidFileNameChars();
        // if file contain illegal chars replave them
        if (illegal.Length > 0 && cleanPath.IndexOfAny(illegal) != -1)
        {
            foreach (char c in illegal)
            {
                if (cleanPath.IndexOf(c) != -1)
                {
                    cleanPath = cleanPath.Replace(c.ToString(), string.Empty);
                }
            }
        }
        cleanPath = cleanPath.Replace(" ", string.Empty);
        string path = _mainPath + cleanPath + ".json";
        Debug.Log(path);
        string json = JsonUtility.ToJson(player);
        File.WriteAllText(path, json);
    }
}
