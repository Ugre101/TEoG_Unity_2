using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FleeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Button btn = null;

    [SerializeField]
    private TextMeshProUGUI quickText = null;

    [SerializeField]
    private KeyCode quickKey = KeyCode.Alpha0;

    private int Roll => Random.Range(0, 100);

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();

        btn.onClick.AddListener(Flee);
        if (quickText != null)
        {
            if (quickKey != KeyCode.None)
            {
                quickText.text = quickKey.ToString().Replace("Alpha", string.Empty).Replace(" ", string.Empty);
            }
            else
            {
                quickText.text = string.Empty;
            }
        }
    }

    public void Flee()
    {
        // TODO add modifers to flee chance
        int myRoll = Roll; // + relevant player skills & perks
        int toBeat = 50; // Mod value dependent on enemies
        if (myRoll >= toBeat)
        {
            CanvasMain.GetCanvasMain.Resume();
        }
        else
        {
            CombatMain.GetCombatMain.PlayerAttack("You failed to escape");
            // Write to combat log and next turn.
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SkillButtonsHoverText.HoverText("Flee\n50% success chance");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SkillButtonsHoverText.StopHovering();
    }
}