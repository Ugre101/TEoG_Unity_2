using System.Collections;
using UnityEngine;

public class SpawnKeyBindings : MonoBehaviour
{
    public KeyBindingButton Prefab;
    public KeyBindingButton bindingButton;
    public KeyBindings keyBindings;
    private KeyCode newKey;
    private bool waitingForKey;

    private enum Keys { save, options, vore, lvl, essence, inventory, esc, quest, looks, zoomIn, zoomOut };

    private Keys keys;
    private KeyBindingButton save, opt, vore, lvl, ess, inv, esc, quest, looks, zoomIn, zoomOut;

    // Start is called before the first frame update
    private void Start()
    {
        save = Instantiate(CreateBindButton("Save", keyBindings.saveKey), transform);
        CreateButtonPart(save, Keys.save);
        opt = Instantiate(CreateBindButton("Options", keyBindings.optionsKey), transform);
        CreateButtonPart(opt, Keys.options);
        vore = Instantiate(CreateBindButton("Vore", keyBindings.voreKey), transform);
        CreateButtonPart(vore, Keys.vore);
        lvl = Instantiate(CreateBindButton("Level up", keyBindings.lvlKey), transform);
        CreateButtonPart(lvl, Keys.lvl);
        ess = Instantiate(CreateBindButton("Essence", keyBindings.essenceKey), transform);
        CreateButtonPart(ess, Keys.essence);
        inv = Instantiate(CreateBindButton("Inventory", keyBindings.inventoryKey), transform);
        CreateButtonPart(inv, Keys.inventory);
        esc = Instantiate(CreateBindButton("Esc", keyBindings.escKey), transform);
        CreateButtonPart(esc, Keys.esc);
        quest = Instantiate(CreateBindButton("Quest", keyBindings.questKey), transform);
        CreateButtonPart(quest, Keys.quest);
        looks = Instantiate(CreateBindButton("Looks", keyBindings.lookKey), transform);
        CreateButtonPart(looks, Keys.looks);
        zoomIn = Instantiate(CreateBindButton("Zoom in", keyBindings.zoomInKey), transform);
        CreateButtonPart(zoomIn, Keys.zoomIn);
        zoomOut = Instantiate(CreateBindButton("Zoom out", keyBindings.zoomOutKey), transform);
        CreateButtonPart(zoomOut, Keys.zoomOut);
    }

    private void OnEnable()
    {
        waitingForKey = false;
    }

    private KeyBindingButton CreateBindButton(string title, KeyCode key)
    {
        KeyBindingButton button = bindingButton;
        button.Title.text = title;
        button.CurKey.text = key.ToString();
        return button;
    }

    private void CreateButtonPart(KeyBindingButton toBind, Keys key)
    {
        toBind.Button.onClick.AddListener(() => WaitFor(key));
    }

    private void WaitFor(Keys key)
    {
        keys = key;
        waitingForKey = true;
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
                ChangeKeyText(ess, newKey);
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

            case Keys.looks:
                keyBindings.lookKey = newKey;
                ChangeKeyText(looks, newKey);
                break;

            case Keys.zoomIn:
                keyBindings.zoomInKey = newKey;
                ChangeKeyText(zoomIn, newKey);
                break;

            case Keys.zoomOut:
                keyBindings.zoomOutKey = newKey;
                ChangeKeyText(zoomOut, newKey);
                break;

            default:
                break;
        }
        yield return null;
    }

    private void ChangeKeyText(KeyBindingButton button, KeyCode newKey)
    {
        button.CurKey.text = newKey.ToString();
    }
}