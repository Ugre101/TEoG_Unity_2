using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoreToggleBtn : MonoBehaviour
{
    public PlayerMain player;
    private Button btn;
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleVore);
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"Vore: {player.Vore.Active}";
    }

    private void ToggleVore()
    {
        text.text = $"Vore: {player.Vore.ToogleVore}";
    }
}