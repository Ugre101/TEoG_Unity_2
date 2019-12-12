using UnityEngine;
using System.Collections.Generic;
// Scriptable object to hold all keybindings
[CreateAssetMenu(fileName = "Key bindings", menuName = "Key bindings")]
public class KeyBindings : ScriptableObject
{
    public KeyBind saveKey = new KeyBind(KeyCode.G,nameof(saveKey));
    public KeyBind optionsKey = new KeyBind(KeyCode.O,nameof(optionsKey));
    public KeyBind voreKey = new KeyBind(KeyCode.V,nameof(voreKey));
    public KeyBind lvlKey = new KeyBind(KeyCode.B,nameof(lvlKey));
    public KeyBind essenceKey = new KeyBind(KeyCode.T,nameof(essenceKey));
    public KeyBind inventoryKey = new KeyBind(KeyCode.Tab,nameof(inventoryKey));
    public KeyBind escKey = new KeyBind(KeyCode.Escape,nameof(escKey));
    public KeyBind questKey = new KeyBind(KeyCode.Q,nameof(questKey));
    public KeyBind mapKey = new KeyBind(KeyCode.M,nameof(mapKey));
    public KeyBind eventKey = new KeyBind(KeyCode.E,nameof(eventKey));
    public KeyBind lookKey = new KeyBind(KeyCode.L,nameof(lookKey));
    public KeyBind zoomInKey = new KeyBind(KeyCode.Comma,nameof(zoomInKey));
    public KeyBind zoomOutKey = new KeyBind(KeyCode.Period,nameof(zoomOutKey));
    public KeyBind hideAllKey = new KeyBind(KeyCode.Space,nameof(hideAllKey));

    private List<KeyBind> keys;
    private void OnEnable()
    {
        keys = new List<KeyBind>() { saveKey, optionsKey, voreKey, lvlKey, essenceKey, inventoryKey, escKey, questKey, mapKey, eventKey, lookKey, zoomInKey, zoomOutKey, hideAllKey };
        keys.ForEach(k => k.Load());
    }

    private void OnDestroy()
    {
        keys.ForEach(k => k.Save());
    }
}