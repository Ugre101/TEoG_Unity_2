﻿using System;
using System.IO;
using UnityEngine;

public class SaveMananger : MonoBehaviour
{
    public static SaveMananger Instance { get; private set; }
    private BasicChar Player => PlayerMain.Player;

    private DirectoryInfo SaveFolder;

    private string newSavePath, lastSavePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SaveFolder = Directory.Exists(SaveSettings.SaveFolder) ? new DirectoryInfo(SaveSettings.SaveFolder) : Directory.CreateDirectory(SaveSettings.SaveFolder);
        Settings.Load();
        Measurements.Load();
    }

    private void OnApplicationQuit()
    {
        Settings.Save();
        Measurements.Save();
    }

    private void Update()
    {
        if (GameManager.KeyBindsActive)
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                QuickSave();
            }
            else if (Input.GetKeyDown(KeyCode.F9))
            {
                QuickLoad();
            }
        }
    }

    private void QuickSave()
    {
        NewSaveGame();
        PopupHandler.GetPopupHandler.SpawnTimedPopup("Saved", 1f);
    }

    public void NewSaveGame()
    {
        SaveName saveName = new SaveName(Player, DateTime.Now);
        newSavePath = SaveFolder.FullName + saveName.CleanSave + ".json";
        File.WriteAllText(newSavePath, NewSave.SaveData());
        SavedEvent?.Invoke();
        lastSavePath = newSavePath;
    }

    public void SaveAndQuit()
    {
        NewSaveGame();
        if (File.Exists(lastSavePath))
        {
            Application.Quit();
        }
        else
        {
            PopupHandler.GetPopupHandler.SpawnTimedPopup("Failed to save, try again or quit without saving.");
        }
    }

    public void QuickLoad()
    {
        if (!string.IsNullOrEmpty(lastSavePath))
        {
            string json = File.ReadAllText(lastSavePath);
            NewSave.LoadData(json);
        }
    }

    public Save NewSave => new Save();

    public delegate void SavedGame();

    public static event SavedGame SavedEvent;
}

public class SaveName
{
    public SaveName(BasicChar player, DateTime parDate)
    {
        Name = player.Identity.FullName;
        Lvl = player.ExpSystem.Level.ToString();
        Date = parDate.ToString();
    }

    private readonly string Name;
    private readonly string Lvl;
    private readonly string Date;

    public string CleanSave
    {
        get
        {
            string cleanNow = Date; //.Remove(Date.Length - 3, 3);
            //    .Replace(":", "-").Replace(" ", "-");
            string cleanPath = $"{Name}-Lvl{Lvl}-{cleanNow}";
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