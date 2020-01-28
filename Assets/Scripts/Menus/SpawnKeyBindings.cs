using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKeyBindings : MonoBehaviour
{
    [SerializeField] private KeyBindingButton prefab = null;

    [SerializeField] private KeyBindings keyBindings = null;

    [SerializeField] private Transform spawnLocation = null;

    private KeyBindingButton selectedBtn;
    private KeyBind selectedKey;
    private KeyCode newKey;
    private bool waitingForKey;
    private bool AltKey = false;
    private readonly List<KeyBindingButton> bindingButtons = new List<KeyBindingButton>();

    // Start is called before the first frame update
    private void Start() => keyBindings.Keys.ForEach(k => SpawnButton(k));

    private void OnEnable() => waitingForKey = false;

    private void SpawnButton(KeyBind parBind)
    {
        KeyBindingButton btn = Instantiate(prefab, spawnLocation);
        btn.Setup(parBind, delegate { WaitFor(parBind, false, btn); }, delegate { WaitFor(parBind, true, btn); });
        bindingButtons.Add(btn);
    }

    private void WaitFor(KeyBind key, bool alt, KeyBindingButton btn)
    {
        selectedKey = key;
        selectedBtn = btn;
        waitingForKey = true;
        AltKey = alt;
        StartCoroutine(GetKey());
    }

    private void OnGUI()
    {
        if (waitingForKey)
        {
            Event keyPressed;
            keyPressed = Event.current;
            if (keyPressed.isKey)
            {
                newKey = keyPressed.keyCode;
                waitingForKey = false;
            }
        }
    }

    public static Action<KeyBind> Affected;

    private IEnumerator GetKey()
    {
        GameManager.KeyBindsActive = false;
        while (waitingForKey)
        {
            yield return null;
        }
        KeyBind effected = AltKey ? keyBindings.AltReBind(selectedKey, newKey) : keyBindings.ReBind(selectedKey, newKey); ;
        if (effected != null)
        {
            Affected?.Invoke(effected);
        }

        GameManager.KeyBindsActive = true;
    }
}