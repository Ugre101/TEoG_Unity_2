using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scriptable object to hold all keybindings
[CreateAssetMenu(fileName ="Key bindings", menuName = "Key bindings")]
public class KeyBindings : ScriptableObject
{
    public KeyCode saveKey;
    public KeyCode optionsKey;
    public KeyCode voreKey;
    public KeyCode lvlKey;
    public KeyCode essenceKey;
    public KeyCode inventoryKey;
    public KeyCode escKey;
    public KeyCode questKey;
    public KeyCode mapKey;
    public KeyCode eventKey;
    public KeyCode lookKey;
    public KeyCode zoomInKey;
    public KeyCode zoomOutKey;
    private void OnEnable()
    {
        // if key saved in playerprefs use that else use default.
        saveKey = PlayerPrefs.HasKey("saveKey") ? (KeyCode)PlayerPrefs.GetInt("saveKey") : KeyCode.G;
        optionsKey = PlayerPrefs.HasKey("optionsKey") ? (KeyCode)PlayerPrefs.GetInt("optionsKey") : KeyCode.O;
        voreKey = PlayerPrefs.HasKey("voreKey") ? (KeyCode)PlayerPrefs.GetInt("voreKey") : KeyCode.V;
        lvlKey = PlayerPrefs.HasKey("lvlKey") ? (KeyCode)PlayerPrefs.GetInt("lvlKey") : KeyCode.B;
        essenceKey = PlayerPrefs.HasKey("essenceKey") ? (KeyCode)PlayerPrefs.GetInt("essenceKey") : KeyCode.T;
        inventoryKey = PlayerPrefs.HasKey("inventoryKey") ? (KeyCode)PlayerPrefs.GetInt("inventoryKey") : KeyCode.Tab;
        escKey = PlayerPrefs.HasKey("escKey") ? (KeyCode)PlayerPrefs.GetInt("escKey") : KeyCode.Escape;
        questKey = PlayerPrefs.HasKey("questKey") ? (KeyCode)PlayerPrefs.GetInt("questKey") : KeyCode.Q;
        mapKey = PlayerPrefs.HasKey("mapKey") ? (KeyCode)PlayerPrefs.GetInt("mapKey") : KeyCode.M;
        eventKey = PlayerPrefs.HasKey("eventKey") ? (KeyCode)PlayerPrefs.GetInt("eventKey") : KeyCode.E;
        lookKey = PlayerPrefs.HasKey("lookKey") ? (KeyCode)PlayerPrefs.GetInt("lookKey") : KeyCode.L;
        zoomInKey = PlayerPrefs.HasKey("zoomInKey") ? (KeyCode)PlayerPrefs.GetInt("zoomInKey") : KeyCode.Comma;
        zoomOutKey = PlayerPrefs.HasKey("zoomOutKey") ? (KeyCode)PlayerPrefs.GetInt("zoomOutKey") : KeyCode.Period;
    }

    private void OnDestroy()
    {
        // Save key binds
        PlayerPrefs.SetInt("saveKey", (int)saveKey);
        PlayerPrefs.SetInt("optionsKey", (int)optionsKey);
        PlayerPrefs.SetInt("voreKey", (int)voreKey);
        PlayerPrefs.SetInt("lvlKey", (int)lvlKey);
        PlayerPrefs.SetInt("essenceKey", (int)essenceKey);
        PlayerPrefs.SetInt("inventoryKey", (int)inventoryKey);
        PlayerPrefs.SetInt("escKey", (int)escKey);
        PlayerPrefs.SetInt("questKey", (int)questKey);
        PlayerPrefs.SetInt("mapKey", (int)mapKey);
        PlayerPrefs.SetInt("eventKey", (int)eventKey);
        PlayerPrefs.SetInt("lookKey", (int)lookKey);
    }
}
