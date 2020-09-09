using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FleeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button btn = null;

    [SerializeField] private TextMeshProUGUI quickText = null;

    [SerializeField] private KeyCode quickKey = KeyCode.Alpha0;

    private static int Roll => Random.Range(0, 100);
    private bool hovering = false;
    private bool hoverBlockActive = false;
    private float timeStartedHovering;

    // Start is called before the first frame update
    private void Start()
    {
        btn = btn != null ? btn : GetComponent<Button>();

        btn.onClick.AddListener(Flee);
        if (quickText == null) return;
        
        quickText.text = quickKey != KeyCode.None ? quickKey.ToString().Replace("Alpha", string.Empty).Replace(" ", string.Empty) : string.Empty;
    }

    private void Update()
    {
        if (!hovering || hoverBlockActive || !(timeStartedHovering + 0.8f <= Time.unscaledTime)) return;
        
        SkillButtonsHoverText.HoverText("Flee\n50% success chance");
        hoverBlockActive = true;
    }

    private static void Flee()
    {
        // TODO add modifers to flee chance
        int myRoll = Roll; // + relevant player skills & perks
        int toBeat = 50; // Mod value dependent on enemies
        if (myRoll >= toBeat)
        {
            GameManager.ReturnToLastState();
        }
        else
        {
            CombatHandler.PlayerAttack("You failed to escape");
            // Write to combat log and next turn.
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        timeStartedHovering = Time.unscaledTime;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        hoverBlockActive = false;
        SkillButtonsHoverText.StopHovering();
    }
}