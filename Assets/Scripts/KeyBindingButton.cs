using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeyBindingButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title = null, curKey = null, curAltKey = null;
    [SerializeField] private Button keyBtn = null, altKeyBtn = null;

    private KeyBind keyBind;

    public void Setup(KeyBind keyBind, UnityAction mainKey, UnityAction altKey)
    {
        this.keyBind = keyBind;
        title.text = keyBind.Title;
        curKey.text = keyBind.Key.ToString();
        curAltKey.text = keyBind.AltKey.ToString();
        keyBtn.onClick.AddListener(mainKey);
        altKeyBtn.onClick.AddListener(altKey);
    }

    private void OnEnable()
    {
        KeyBindings.Affected += IsAffected;
        KeyBindings.AltAffected += IsAltAffected;
    }

    private void OnDisable()
    {
        KeyBindings.Affected -= IsAffected;
        KeyBindings.AltAffected -= IsAltAffected;
    }

    public void SetKeyText(KeyCode keyCode) => curKey.text = keyCode.ToString();

    public void SetAltKeyText(KeyCode keyCode) => curAltKey.text = keyCode.ToString();

    private void IsAffected(KeyBind btn)
    {
        if (btn == keyBind && keyBind.Key == KeyCode.None)
        {
            curKey.text = KeyCode.None.ToString();
        }
    }

    private void IsAltAffected(KeyBind btn)
    {
        if (btn == keyBind && keyBind.AltKey == KeyCode.None)
        {
            curAltKey.text = KeyCode.None.ToString();
        }
    }
}