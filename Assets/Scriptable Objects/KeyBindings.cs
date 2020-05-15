using System.Collections.Generic;
using UnityEngine;

public static class KeyBindings
{
    private static List<KeyBind> keys;

    public static List<KeyBind> Keys
    {
        get
        {
            if (keys == null)
            {
                keys = new List<KeyBind>() { SaveKey, OptionsKey, VoreKey, LvlKey, EssenceKey, InventoryKey, EscKey, QuestKey, MapKey, EventKey, LookKey, ZoomInKey, ZoomOutKey, HideAllKey, QuickSave, QuickLoad, ActionKey };
            }
            return keys;
        }
    }

    public static KeyBind SaveKey { get; } = new KeyBind(KeyCode.G, "Save");
    public static KeyBind OptionsKey { get; } = new KeyBind(KeyCode.O, ("Options"));
    public static KeyBind VoreKey { get; } = new KeyBind(KeyCode.V, "Vore");
    public static KeyBind LvlKey { get; } = new KeyBind(KeyCode.B, "Perks");
    public static KeyBind EssenceKey { get; } = new KeyBind(KeyCode.T, "Essence");
    public static KeyBind InventoryKey { get; } = new KeyBind(KeyCode.Tab, "Inventory");
    public static KeyBind EscKey { get; } = new KeyBind(KeyCode.Escape, "Esc");
    public static KeyBind QuestKey { get; } = new KeyBind(KeyCode.Q, "Quests");
    public static KeyBind MapKey { get; } = new KeyBind(KeyCode.M, "Map");
    public static KeyBind EventKey { get; } = new KeyBind(KeyCode.E, "Eventlog");
    public static KeyBind LookKey { get; } = new KeyBind(KeyCode.L, "Looks");
    public static KeyBind ZoomInKey { get; } = new KeyBind(KeyCode.Comma, "Zoom in");
    public static KeyBind ZoomOutKey { get; } = new KeyBind(KeyCode.Period, "Zoom out");
    public static KeyBind HideAllKey { get; } = new KeyBind(KeyCode.Space, "Hide UI");
    public static KeyBind QuickSave { get; } = new KeyBind(KeyCode.F5, "Quick save");
    public static KeyBind QuickLoad { get; } = new KeyBind(KeyCode.F9, "Quick load");
    public static KeyBind ActionKey { get; } = new KeyBind(KeyCode.F, "Action");

    public static KeyBind ReBind(KeyBind toReBind, KeyCode newKeyCode)
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

    public static KeyBind AltReBind(KeyBind toReBind, KeyCode newKeyCode)
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
}