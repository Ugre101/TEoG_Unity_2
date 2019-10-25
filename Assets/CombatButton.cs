using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public BasicSkill skill;
    public TextMeshProUGUI text, keycode;
    public CombatButtons combatButtons;
    public Button btn;
    public Image img;
    public KeyCode quickKey;
    public SkillButtons skillButtons;
    private playerMain player => combatButtons.player;
    private BasicChar target => combatButtons.CurrentEnemy;

    // Start is called before the first frame update
    private void Start()
    {
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

    public void Setup()
    {
        text.text = skill.Title;
        img.sprite = skill.Icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            skillButtons.ToogleChooseSkill(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        skillButtons.EnableHoverText($"{skill.Title}\n{skill.Type}\n{skill.BaseAttack}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        skillButtons.DisableHoverText();
    }
}