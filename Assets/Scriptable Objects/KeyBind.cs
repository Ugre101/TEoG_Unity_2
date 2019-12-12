using UnityEngine;

[System.Serializable]
public class KeyBind
{
    private string saveName;

    [SerializeField]
    private KeyCode mykey;

    public KeyCode Key => mykey;

    public void ReBind(KeyCode parKey)
    {
        mykey = parKey;
    }

    public KeyBind(KeyCode parKey, string parName)
    {
        mykey = parKey;
        saveName = parName;
    }

    public void Save() => PlayerPrefs.SetInt(saveName, (int)mykey);

    public void Load()
    {
        if (PlayerPrefs.HasKey(saveName))
        {
            mykey = (KeyCode)PlayerPrefs.GetInt(saveName);
        }
    }
}