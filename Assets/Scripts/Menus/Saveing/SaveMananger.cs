using System;
using System.IO;
using UnityEngine;

public class SaveMananger : MonoBehaviour
{
    public playerMain player;
    public Transform playerSprite;
    public Dorm dorm;
    public GameUI gameUI;
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
        Save save = new Save(player, playerSprite,dorm);
        File.WriteAllText(path, save.SaveData());
    }
}