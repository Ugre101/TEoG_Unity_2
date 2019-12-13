using UnityEngine;

[System.Serializable]
public class KeyBind
{
    public readonly string Title;
    public string SaveName => Title.Replace(" ", string.Empty);
    public string AltSaveName => Title.Replace(" ", string.Empty) + "Alt";

    public KeyCode Key { get; private set; }
    public KeyCode AltKey { get; private set; } = KeyCode.None;

    public void ReBind(KeyCode parKey)
    {
        Key = parKey;
        PlayerPrefs.SetInt(SaveName, (int)parKey);
    }

    public void ReBindAlt(KeyCode parKey)
    {
        AltKey = parKey;
        PlayerPrefs.SetInt(AltSaveName, (int)parKey);
    }

    public KeyBind(KeyCode parKey, string parTitle)
    {
        Key = parKey;
        Title = parTitle;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(SaveName, (int)Key);
        if (AltKey != KeyCode.None)
        {
            PlayerPrefs.SetInt(AltSaveName, (int)AltKey);
        }
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(SaveName))
        {
            Key = (KeyCode)PlayerPrefs.GetInt(SaveName);
        }
        if (PlayerPrefs.HasKey(AltSaveName))
        {
            AltKey = (KeyCode)PlayerPrefs.GetInt(AltSaveName);
        }
    }

    public bool GetKeyDown() => Input.GetKeyDown(Key) || Input.GetKeyDown(AltKey);

    public bool GetKeyUp() => Input.GetKeyUp(Key) || Input.GetKeyUp(AltKey);

    public bool GetKey() => Input.GetKey(Key) || Input.GetKey(AltKey);
}