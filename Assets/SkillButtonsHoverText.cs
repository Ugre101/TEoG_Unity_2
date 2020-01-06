using TMPro;
using UnityEngine;

public class SkillButtonsHoverText : MonoBehaviour
{
    public static SkillButtonsHoverText GetSkillButtonsHoverText { get; private set; }
    private static TextMeshProUGUI TextBox;

    public static void HoverText(string content)
    {
        TextBox.text = content;
        GetSkillButtonsHoverText.gameObject.SetActive(true);
    }

    public static void StopHovering()
    {
        GetSkillButtonsHoverText.gameObject.SetActive(false);
    }
    [SerializeField]
    private TextMeshProUGUI textbox = null;

    // Start is called before the first frame update
    private void Start()
    {
        if (GetSkillButtonsHoverText == null)
        {
            GetSkillButtonsHoverText = this;
        }
        else if (GetSkillButtonsHoverText != this)
        {
            Destroy(gameObject);
        }
        textbox = textbox != null ? textbox : GetComponentInChildren<TextMeshProUGUI>();
        TextBox = textbox;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}