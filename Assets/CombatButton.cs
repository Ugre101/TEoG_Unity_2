using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UserSkill userSkill;
    public BasicSkill skill => userSkill.skill;
    public TextMeshProUGUI text, keycode;
    public CombatButtons combatButtons;
    public Button btn;
    public Image img;
    public KeyCode quickKey;
    public SkillButtons skillButtons;
    private playerMain player => combatButtons.player;
    private BasicChar target => combatButtons.CurrentEnemy;
    private bool hovering;
    private bool hoverBlockActive = false;
    private float timeStarted;

    // CoolDown

    // Start is called before the first frame update
    private void Start()
    {
        btn.onClick.AddListener(Click);
        if (skill != null) { Setup(); }
        else { text.text = null; img.gameObject.SetActive(false); }
    }

    private void Click()
    {
        Debug.Log(skill);
        if (skill != null) { combatButtons.PlayerAttack(skill.Action(player, target)); }
        else
        {
            skillButtons.ToogleChooseSkill(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(quickKey))
        {
            Click();
        }
        // Delay before starting hoverText
        if (hovering)
        {
            if (timeStarted + 0.8f <= Time.unscaledTime)
            {
                if (!hoverBlockActive)
                {
                    skillButtons.EnableHoverText($"{skill.Title}\n{skill.Type}\n{skill.BaseAttack}");
                    hoverBlockActive = true;
                }
            }
        }
    }

    public void Setup()
    {
        text.text = skill.Title;
        img.gameObject.SetActive(true);
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
        if (skill != null)
        {
            hovering = true;
            timeStarted = Time.unscaledTime;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        hoverBlockActive = false;
        skillButtons.DisableHoverText();
    }
}