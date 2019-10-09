using UnityEngine;

// Scriptable object to hold all keybindings
[CreateAssetMenu(fileName = "Key bindings", menuName = "Key bindings")]
public class KeyBindings : ScriptableObject
{
    public KeyCode saveKey = KeyCode.G;
    public KeyCode optionsKey = KeyCode.O;
    public KeyCode voreKey = KeyCode.V;
    public KeyCode lvlKey = KeyCode.B;
    public KeyCode essenceKey = KeyCode.T;
    public KeyCode inventoryKey = KeyCode.Tab;
    public KeyCode escKey = KeyCode.Escape;
    public KeyCode questKey = KeyCode.Q;
    public KeyCode mapKey = KeyCode.M;
    public KeyCode eventKey = KeyCode.E;
    public KeyCode lookKey = KeyCode.L;
    public KeyCode zoomInKey = KeyCode.Comma;
    public KeyCode zoomOutKey = KeyCode.Period;
    public KeyCode hideAllKey = KeyCode.Space;

    private void OnEnable()
    {
        // if key saved in playerprefs use that else use default.
        saveKey = HasSavedKey(nameof(saveKey), saveKey);
        optionsKey = HasSavedKey(nameof(optionsKey), optionsKey);
        voreKey = HasSavedKey(nameof(voreKey), voreKey);
        lvlKey = HasSavedKey(nameof(lvlKey), lvlKey);
        essenceKey = HasSavedKey(nameof(essenceKey), essenceKey);
        inventoryKey = HasSavedKey(nameof(inventoryKey), inventoryKey);
        escKey = HasSavedKey(nameof(escKey), escKey);
        questKey = HasSavedKey(nameof(questKey), questKey);
        mapKey = HasSavedKey(nameof(mapKey), mapKey);
        eventKey = HasSavedKey(nameof(eventKey), eventKey);
        lookKey = HasSavedKey(nameof(lookKey), lookKey);
        zoomInKey = HasSavedKey(nameof(zoomInKey), zoomInKey);
        zoomOutKey = HasSavedKey(nameof(zoomOutKey), zoomOutKey);
        hideAllKey = HasSavedKey(nameof(hideAllKey), hideAllKey);
    }

    private void OnDestroy()
    {
        // Save key binds
        KeySave(nameof(saveKey), saveKey);
        KeySave(nameof(optionsKey), optionsKey);
        KeySave(nameof(voreKey), voreKey);
        KeySave(nameof(lvlKey), lvlKey);
        KeySave(nameof(essenceKey), essenceKey);
        KeySave(nameof(inventoryKey), inventoryKey);
        KeySave(nameof(escKey), escKey);
        KeySave(nameof(questKey), questKey);
        KeySave(nameof(mapKey), mapKey);
        KeySave(nameof(eventKey), eventKey);
        KeySave(nameof(lookKey), lookKey);
        KeySave(nameof(hideAllKey), hideAllKey);
    }

    private KeyCode HasSavedKey(string name, KeyCode key)
    {
        Debug.Log(name);
        return PlayerPrefs.HasKey(name) ? (KeyCode)PlayerPrefs.GetInt(name) : key;
    }

    private void KeySave(string name, KeyCode key)
    {
        PlayerPrefs.SetInt(name, (int)key);
    }
}