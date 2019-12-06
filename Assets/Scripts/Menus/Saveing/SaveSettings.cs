using UnityEngine;

public static class SaveSettings
{
    // saveing path here to ensure I always have same path.
    public static string SaveFolder => Application.persistentDataPath + "/Game_Save/";
}