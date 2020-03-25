using UnityEngine;

[System.Serializable]
public class KeyBind
{
    [SerializeField] private string title;
    [SerializeField] private KeyCode key;
    [SerializeField] private KeyCode altKey = KeyCode.None;

    public string Title => title;
    private string SaveName => Title.Replace(" ", string.Empty);
    private string AltSaveName => Title.Replace(" ", string.Empty) + "Alt";

    public KeyCode Key { get => key; private set => key = value; }
    public KeyCode AltKey { get => altKey; private set => altKey = value; }

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
        title = parTitle;
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

    public bool KeyDown => Input.GetKeyDown(Key) || Input.GetKeyDown(AltKey);

    public bool KeyUp => Input.GetKeyUp(Key) || Input.GetKeyUp(AltKey);

    public bool GetsKey => Input.GetKey(Key) || Input.GetKey(AltKey);
}