using System.Collections.Generic;
using UnityEngine;

// Scriptable object to hold all keybindings
[CreateAssetMenu(fileName = "Key bindings", menuName = "Key bindings")]
public class KeyBindings : ScriptableObject
{
    public KeyBind saveKey = new KeyBind(KeyCode.G, "Save");
    public KeyBind optionsKey = new KeyBind(KeyCode.O, ("Options"));
    public KeyBind voreKey = new KeyBind(KeyCode.V, "Vore");
    public KeyBind lvlKey = new KeyBind(KeyCode.B, "Perks");
    public KeyBind essenceKey = new KeyBind(KeyCode.T, "Essence");
    public KeyBind inventoryKey = new KeyBind(KeyCode.Tab, "Inventory");
    public KeyBind escKey = new KeyBind(KeyCode.Escape, "Esc");
    public KeyBind questKey = new KeyBind(KeyCode.Q, "Quests");
    public KeyBind mapKey = new KeyBind(KeyCode.M, "Map");
    public KeyBind eventKey = new KeyBind(KeyCode.E, "Eventlog");
    public KeyBind lookKey = new KeyBind(KeyCode.L, "Looks");
    public KeyBind zoomInKey = new KeyBind(KeyCode.Comma, "Zoom in");
    public KeyBind zoomOutKey = new KeyBind(KeyCode.Period, "Zoom out");
    public KeyBind hideAllKey = new KeyBind(KeyCode.Space, "Hide UI");

    public List<KeyBind> Keys { get; private set; }

    private void OnEnable()
    {
        Keys = new List<KeyBind>() { saveKey, optionsKey, voreKey, lvlKey, essenceKey, inventoryKey, escKey, questKey, mapKey, eventKey, lookKey, zoomInKey, zoomOutKey, hideAllKey };
        Keys.ForEach(k => k.Load());
    }

    public KeyBind ReBind(KeyBind toReBind, KeyCode newKeyCode)
    {
        KeyBind effected = null;
        if (Keys.Exists(k => k.Key == newKeyCode))
        {
            effected = Keys.Find(k => k.Key == newKeyCode);
            effected.ReBind(KeyCode.None);
        }
        else if (Keys.Exists(k => k.AltKey == newKeyCode))
        {
            effected = Keys.Find(k => k.AltKey == newKeyCode);
            effected.ReBindAlt(KeyCode.None);
        }
        toReBind.ReBind(newKeyCode);
        return effected;
    }

    public KeyBind AltReBind(KeyBind toReBind, KeyCode newKeyCode)
    {
        KeyBind effected = null;
        if (Keys.Exists(k => k.Key == newKeyCode))
        {
            effected = Keys.Find(k => k.Key == newKeyCode);
            effected.ReBind(KeyCode.None);
        }
        else if (Keys.Exists(k => k.AltKey == newKeyCode))
        {
            effected = Keys.Find(k => k.AltKey == newKeyCode);
            effected.ReBindAlt(KeyCode.None);
        }
        toReBind.ReBindAlt(newKeyCode);
        return effected;
    }

    private void OnDestroy()
    {
        Keys.ForEach(k => k.Save());
    }
}