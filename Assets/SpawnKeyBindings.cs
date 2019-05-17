using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnKeyBindings : MonoBehaviour
{
    public GameObject Prefab;
    public KeyBindings keyBindings;
    private KeyCode newKey;
    private bool waitingForKey;

    private enum Keys { save, options, vore, lvl, essence, inventory, esc, quest };

    private Keys keys;
    private GameObject save, opt, vore, lvl, ess, inv, esc, quest;

    // Start is called before the first frame update
    private void Start()
    {
        save = Instantiate(CreateBindButton("Save", keyBindings.saveKey), this.transform);
        Button savebtn = CreateButtonPart(save, Keys.save);
        opt = Instantiate(CreateBindButton("Options", keyBindings.optionsKey), this.transform);
        Button optbtn = CreateButtonPart(opt, Keys.options);
        vore = Instantiate(CreateBindButton("Vore", keyBindings.voreKey), this.transform);
        Button vorebtn = CreateButtonPart(vore, Keys.vore);
        lvl = Instantiate(CreateBindButton("Level up", keyBindings.lvlKey), this.transform);
        Button lvlbtn = CreateButtonPart(lvl, Keys.lvl);
        ess = Instantiate(CreateBindButton("Essence", keyBindings.essenceKey), this.transform);
        Button essbtn = CreateButtonPart(ess, Keys.essence);
        inv = Instantiate(CreateBindButton("Inventory", keyBindings.inventoryKey), this.transform);
        Button invbtn = CreateButtonPart(inv, Keys.inventory);
        esc = Instantiate(CreateBindButton("Esc", keyBindings.escKey), this.transform);
        Button escbtn = CreateButtonPart(esc, Keys.esc);
        quest = Instantiate(CreateBindButton("Quest", keyBindings.questKey), this.transform);
        Button questbtn = CreateButtonPart(quest, Keys.quest);
    }

    private void OnEnable()
    {
        waitingForKey = false;
    }

    private GameObject CreateBindButton(string title, KeyCode key)
    {
        GameObject button = Prefab;
        TextMeshProUGUI[] texts = button.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI Title = texts[0];
        TextMeshProUGUI Key = texts[1];
        Title.text = title;
        Key.text = key.ToString();
        return button;
    }

    private Button CreateButtonPart(GameObject toBind, Keys key)
    {
        Button btn = toBind.GetComponentInChildren<Button>();
        btn.onClick.AddListener(() => WaitFor(key));
        return btn;
    }

    private void WaitFor(Keys key)
    {
        keys = key;
        waitingForKey = true;
        StartCoroutine(getKey());
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

    private IEnumerator getKey()
    {
        while (waitingForKey)
        {
            yield return null;
        }
        switch (keys)
        {
            case Keys.esc:
                keyBindings.escKey = newKey;
                ChangeKeyText(esc, newKey);
                break;

            case Keys.essence:
                keyBindings.essenceKey = newKey;
                ChangeKeyText(esc, newKey);
                break;

            case Keys.inventory:
                keyBindings.inventoryKey = newKey;
                ChangeKeyText(inv, newKey);
                break;

            case Keys.lvl:
                keyBindings.lvlKey = newKey;
                ChangeKeyText(lvl, newKey);
                break;

            case Keys.options:
                keyBindings.optionsKey = newKey;
                ChangeKeyText(opt, newKey);
                break;

            case Keys.quest:
                keyBindings.questKey = newKey;
                ChangeKeyText(quest, newKey);
                break;

            case Keys.save:
                keyBindings.saveKey = newKey;
                ChangeKeyText(save, newKey);
                break;

            case Keys.vore:
                keyBindings.voreKey = newKey;
                ChangeKeyText(vore, newKey);
                break;

            default:
                break;
        }
        yield return null;
    }

    private void ChangeKeyText(GameObject button, KeyCode newKey)
    {
        TextMeshProUGUI[] texts = button.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI Key = texts[1];
        Key.text = newKey.ToString();
    }
}