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
        save = Instantiate(CreateBindButton("Save", keyBindings.saveKey.Key), transform);
        CreateButtonPart(save, Keys.save);
        opt = Instantiate(CreateBindButton("Options", keyBindings.optionsKey.Key), transform);
        CreateButtonPart(opt, Keys.options);
        vore = Instantiate(CreateBindButton("Vore", keyBindings.voreKey.Key), transform);
        CreateButtonPart(vore, Keys.vore);
        lvl = Instantiate(CreateBindButton("Level up", keyBindings.lvlKey.Key), transform);
        CreateButtonPart(lvl, Keys.lvl);
        ess = Instantiate(CreateBindButton("Essence", keyBindings.essenceKey.Key), transform);
        CreateButtonPart(ess, Keys.essence);
        inv = Instantiate(CreateBindButton("Inventory", keyBindings.inventoryKey.Key), transform);
        CreateButtonPart(inv, Keys.inventory);
        esc = Instantiate(CreateBindButton("Esc", keyBindings.escKey.Key), transform);
        CreateButtonPart(esc, Keys.esc);
        quest = Instantiate(CreateBindButton("Quest", keyBindings.questKey.Key), transform);
        CreateButtonPart(quest, Keys.quest);
        looks = Instantiate(CreateBindButton("Looks", keyBindings.lookKey.Key), transform);
        CreateButtonPart(looks, Keys.looks);
        zoomIn = Instantiate(CreateBindButton("Zoom in", keyBindings.zoomInKey.Key), transform);
        CreateButtonPart(zoomIn, Keys.zoomIn);
        zoomOut = Instantiate(CreateBindButton("Zoom out", keyBindings.zoomOutKey.Key), transform);
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
                keyBindings.escKey.ReBind(newKey);
                ChangeKeyText(esc, newKey);
                break;

            case Keys.essence:
                keyBindings.essenceKey.ReBind(newKey);
                ChangeKeyText(ess, newKey);
                break;

            case Keys.inventory:
                keyBindings.inventoryKey.ReBind(newKey);
                ChangeKeyText(inv, newKey);
                break;

            case Keys.lvl:
                keyBindings.lvlKey.ReBind(newKey);
                ChangeKeyText(lvl, newKey);
                break;

            case Keys.options:
                keyBindings.optionsKey.ReBind(newKey);
                ChangeKeyText(opt, newKey);
                break;

            case Keys.quest:
                keyBindings.questKey.ReBind(newKey);
                ChangeKeyText(quest, newKey);
                break;

            case Keys.save:
                keyBindings.saveKey.ReBind(newKey);
                ChangeKeyText(save, newKey);
                break;

            case Keys.vore:
                keyBindings.voreKey.ReBind(newKey);
                ChangeKeyText(vore, newKey);
                break;

            case Keys.looks:
                keyBindings.lookKey.ReBind(newKey);
                ChangeKeyText(looks, newKey);
                break;

            case Keys.zoomIn:
                keyBindings.zoomInKey.ReBind(newKey);
                ChangeKeyText(zoomIn, newKey);
                break;

            case Keys.zoomOut:
                keyBindings.zoomOutKey.ReBind(newKey);
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