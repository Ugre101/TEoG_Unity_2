using System.Collections.Generic;
using UnityEngine;

// Scriptable object to hold all keybindings
[CreateAssetMenu(fileName = "Key bindings", menuName = "Key bindings")]
public class KeyBindings : ScriptableObject
{
    [SerializeField]
    private KeyBind saveKey = new KeyBind(KeyCode.G, "Save"), optionsKey = new KeyBind(KeyCode.O, ("Options")), voreKey = new KeyBind(KeyCode.V, "Vore"), lvlKey = new KeyBind(KeyCode.B, "Perks"), essenceKey = new KeyBind(KeyCode.T, "Essence"), inventoryKey = new KeyBind(KeyCode.Tab, "Inventory"),
     escKey = new KeyBind(KeyCode.Escape, "Esc"), questKey = new KeyBind(KeyCode.Q, "Quests"), mapKey = new KeyBind(KeyCode.M, "Map"),
     eventKey = new KeyBind(KeyCode.E, "Eventlog"), lookKey = new KeyBind(KeyCode.L, "Looks"), zoomInKey = new KeyBind(KeyCode.Comma, "Zoom in"),
     zoomOutKey = new KeyBind(KeyCode.Period, "Zoom out"), hideAllKey = new KeyBind(KeyCode.Space, "Hide UI"), quickSave = new KeyBind(KeyCode.F5, "Quick save"),
     quickLoad = new KeyBind(KeyCode.F9, "Quick load");

    public List<KeyBind> Keys { get; private set; }
    public KeyBind SaveKey => saveKey;
    public KeyBind OptionsKey => optionsKey;
    public KeyBind VoreKey => voreKey;
    public KeyBind LvlKey => lvlKey;
    public KeyBind EssenceKey => essenceKey;
    public KeyBind InventoryKey => inventoryKey;
    public KeyBind EscKey => escKey;
    public KeyBind QuestKey => questKey;
    public KeyBind MapKey => mapKey;
    public KeyBind EventKey => eventKey;
    public KeyBind LookKey => lookKey;
    public KeyBind ZoomInKey => zoomInKey;
    public KeyBind ZoomOutKey => zoomOutKey;
    public KeyBind HideAllKey => hideAllKey;
    public KeyBind QuickSave => quickSave;
    public KeyBind QuickLoad => quickLoad;

    private void OnEnable()
    {
        Keys = new List<KeyBind>() { SaveKey, OptionsKey, VoreKey, LvlKey, EssenceKey, InventoryKey, EscKey, QuestKey, MapKey, EventKey, LookKey, ZoomInKey, ZoomOutKey, HideAllKey, QuickSave, QuickLoad };
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