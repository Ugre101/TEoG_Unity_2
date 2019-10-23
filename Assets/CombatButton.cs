using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CombatButton : MonoBehaviour,IPointerClickHandler
{
    public BasicSkill skill;
    public TextMeshProUGUI text;
    private CombatButtons combatButtons;
    public Button btn;
    public Image img;
    public KeyCode quickKey;
    private playerMain player => combatButtons.player;
    private BasicChar target => combatButtons.CurrentEnemy;

    // Start is called before the first frame update
    private void Start()
    {
        combatButtons = GetComponentInParent<CombatButtons>();
        btn.onClick.AddListener(Click);
        if (skill != null) { Setup(); }
    }

    private void Click()
    {
        if (skill != null) { combatButtons.PlayerAttack(skill.Action(player, target)); }
        else
        {
            // Chose skill
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(quickKey))
        {
            Click();
        }
    }

    private void Setup()
    {
        text.text = skill.Title;
        img.sprite = skill.Icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Chose new skill, when implemented.");
        }
    }
}