using System.Collections.Generic;
using UnityEngine;

public class SpawnKeyBindings : MonoBehaviour
{
    [SerializeField] private KeyBindingButton prefab = null;

    [SerializeField] private Transform spawnLocation = null;

    private KeyBindingButton selectedBtn;
    private KeyBind selectedKey;
    private bool waitingForKey, altKey;
    private readonly List<KeyBindingButton> bindingButtons = new List<KeyBindingButton>();

    // Start is called before the first frame update
    private void Start() => KeyBindings.Keys.ForEach(k => SpawnButton(k));

    private void OnEnable() => waitingForKey = false;

    private void SpawnButton(KeyBind parBind)
    {
        KeyBindingButton btn = Instantiate(prefab, spawnLocation);
        btn.Setup(parBind, () => WaitForMain(parBind, btn), () => WaitForAlt(parBind, btn));
        bindingButtons.Add(btn);
    }

    private void WaitForShared(KeyBind key, KeyBindingButton btn)
    {
        selectedKey = key;
        selectedBtn = btn;
        waitingForKey = true;
        GameManager.KeyBindsActive = false;
    }

    private void WaitForMain(KeyBind key, KeyBindingButton btn)
    {
        WaitForShared(key, btn);
        altKey = false;
    }

    private void WaitForAlt(KeyBind key, KeyBindingButton btn)
    {
        WaitForShared(key, btn);
        altKey = true;
    }

    // Switched to update as I didn't like having an potential unresolved corutine and I want it to be possible to bind mouse keys expect mouse 0 & 1
    private void Update()
    {
        if (waitingForKey && Input.anyKeyDown)
        {
            UgreTools.EnumToList<KeyCode>().ForEach(k => GetKey(k));
        }
    }

    private List<KeyCode> notAllowed = new List<KeyCode>() { KeyCode.Mouse0, KeyCode.Mouse1 };

    private void GetKey(KeyCode k)
    {
        if (!Input.GetKeyDown(k) || notAllowed.Contains(k))
        {
            return;
        }
        if (!altKey)
        {
            KeyBindings.AltReBind(selectedKey, k);
            selectedBtn.SetAltKeyText(k);
        }
        else
        {
            KeyBindings.ReBind(selectedKey, k);
            selectedBtn.SetKeyText(k);
        }
        GameManager.KeyBindsActive = true;
    }
}