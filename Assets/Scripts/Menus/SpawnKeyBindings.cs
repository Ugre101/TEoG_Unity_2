using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKeyBindings : MonoBehaviour
{
    [SerializeField]
    private KeyBindingButton prefab = null;

    [SerializeField]
    private KeyBindings keyBindings = null;

    [SerializeField]
    private Transform spawnLocation = null;

    private KeyBindingButton selectedBtn;
    private KeyBind selectedKey;
    private KeyCode newKey;
    private bool waitingForKey;
    private bool AltKey = false;
    private readonly List<KeyBindingButton> bindingButtons = new List<KeyBindingButton>();

    // Start is called before the first frame update
    private void Start()
    {
        keyBindings.Keys.ForEach(k => SpawnButton(k));
    }

    private void OnEnable()
    {
        waitingForKey = false;
    }

    private void SpawnButton(KeyBind parBind)
    {
        KeyBindingButton btn = Instantiate(prefab, spawnLocation);
        btn.Title.text = parBind.Title;
        btn.CurKey.text = parBind.Key.ToString();
        btn.CurAltKey.text = parBind.AltKey.ToString();
        btn.KeyBtn.onClick.AddListener(() => WaitFor(parBind, false, btn));
        btn.AltKeyBtn.onClick.AddListener(() => WaitFor(parBind, true, btn));
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

    private IEnumerator GetKey()
    {
        GameManager.KeyBindsActive = false;
        while (waitingForKey)
        {
            yield return null;
        }
        if (AltKey)
        {
            KeyBind effected = keyBindings.AltReBind(selectedKey, newKey);
            if (effected != null)
            {
                bindingButtons.Find(bb => bb.Title.text.Equals(effected.Title)).CurAltKey.text = KeyCode.None.ToString();
            }
            selectedBtn.CurAltKey.text = newKey.ToString();
        }
        else
        {
            KeyBind effected = keyBindings.ReBind(selectedKey, newKey);
            if (effected != null)
            {
                bindingButtons.Find(bb => bb.Title.text.Equals(effected.Title)).CurKey.text = KeyCode.None.ToString();
            }
            selectedBtn.CurKey.text = newKey.ToString();
        }
        GameManager.KeyBindsActive = true;
    }
}