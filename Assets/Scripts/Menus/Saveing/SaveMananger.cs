using System;
using System.IO;
using UnityEngine;

public class SaveMananger : MonoBehaviour
{
    public PlayerMain player;
    public Transform playerSprite;
    public Dorm dorm;
    public GameUI gameUI;
    public MapEvents mapEvents;
    public TickManager tickManager;
    public Home home;
    public EventLog eventLog;
    public string SaveFolder => Application.persistentDataPath + "/Game_Save/";
    private string lastSavePath;

    private void Start()
    {
        if (!Directory.Exists(SaveFolder))
        {
            _ = Directory.CreateDirectory(SaveFolder);
        }
        Settings.SetImperial = PlayerPrefs.HasKey("Imperial") ? PlayerPrefs.GetInt("Imperial") == 1 : false; ;
    }

    public void NewSaveGame()
    {
        SaveName saveName = new SaveName(player, DateTime.Now);
        lastSavePath = SaveFolder + saveName.CleanSave + ".json";
        Save save = new Save(player, playerSprite, dorm, mapEvents, tickManager, home, eventLog);
        File.WriteAllText(lastSavePath, save.SaveData());
    }

    public void SaveAndQuit()
    {
        NewSaveGame();
        if (File.Exists(lastSavePath))
        {
            Debug.Log("Pause menu; save & quit");
            Application.Quit();
        }
    }
}

[Serializable]
public class SaveName
{
    public SaveName(PlayerMain player, DateTime parDate)
    {
        Name = player.FullName;
        Lvl = player.ExpSystem.Level.ToString();
        Date = parDate.ToString();
    }

    public string Name, Lvl, Date;

    public string CleanSave
    {
        get
        {
            string cleanNow = Date.Remove(Date.Length - 3, 3);
            //    .Replace(":", "-").Replace(" ", "-");
            string cleanPath = Name + "-Lvl" + Lvl + "-" + cleanNow;
            char[] illegal = Path.GetInvalidFileNameChars();
            // if file contain illegal chars replave them
            if (illegal.Length > 0 && cleanPath.IndexOfAny(illegal) != -1)
            {
                foreach (char c in illegal)
                {
                    if (cleanPath.IndexOf(c) != -1)
                    {
                        cleanPath = cleanPath.Replace(c.ToString(), "-");
                    }
                }
            }
            cleanPath = cleanPath.Replace(" ", "-");
            return cleanPath;
        }
    }
}