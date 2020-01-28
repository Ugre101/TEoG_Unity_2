using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeyBindingButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title = null;
    [SerializeField] private TextMeshProUGUI curKey = null;
    [SerializeField] private TextMeshProUGUI curAltKey = null;
    [SerializeField] private Button keyBtn = null;
    [SerializeField] private Button altKeyBtn = null;

    private KeyBind keyBind;

    public void Setup(KeyBind keyBind, UnityAction mainKey, UnityAction altKey)
    {
        this.keyBind = keyBind;
        title.text = keyBind.Title;
        curKey.text = keyBind.Key.ToString();
        curAltKey.text = keyBind.AltKey.ToString();
        keyBtn.onClick.AddListener(mainKey);
        altKeyBtn.onClick.AddListener(altKey);

        SpawnKeyBindings.Affected += IsAffected;
    }

    public void SetKeyText(KeyCode keyCode, bool alt)
    {
        if (alt)
        {
            curAltKey.text = keyCode.ToString();
        }
        else
        {
            curKey.text = keyCode.ToString();
        }
    }

    private void IsAffected(KeyBind btn)
    {
        if (btn == keyBind)
        {
            if (keyBind.AltKey == KeyCode.None)
            {
                curAltKey.text = KeyCode.None.ToString();
            }
            else if (keyBind.Key == KeyCode.None)
            {
                curKey.text = KeyCode.None.ToString();
            }
        }
    }
}