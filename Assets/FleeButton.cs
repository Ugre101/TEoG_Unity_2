using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FleeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public playerMain player;
    public GameUI gameUI;
    public CombatButtons combatButtons;
    public Button btn;
    public KeyCode quickKey;
    public SkillButtons skillButtons;
    private int Roll => Random.Range(0, 100);

    // Start is called before the first frame update
    private void Start()
    {
        btn.onClick.AddListener(Flee);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Flee()
    {
        int myRoll = Roll; // + relevant player skills & perks
        int toBeat = 50; // Mod value dependent on enemies
        if (myRoll >= toBeat)
        {
            gameUI.Resume();
        }
        else
        {
            combatButtons.PlayerAttack("You failed to escape");
            // Write to combat log and next turn.
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        skillButtons.EnableHoverText("Flee\n50% success chance");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skillButtons.DisableHoverText();
    }
}