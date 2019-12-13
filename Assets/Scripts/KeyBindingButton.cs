using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingButton : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI Title { get; private set; } = null;
    [field: SerializeField] public TextMeshProUGUI CurKey { get; private set; } = null;
    [field: SerializeField] public TextMeshProUGUI CurAltKey { get; private set; } = null;
    [field: SerializeField] public Button KeyBtn { get; private set; } = null;
    [field: SerializeField] public Button AltKeyBtn { get; private set; } = null;
}