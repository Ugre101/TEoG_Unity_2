using SkillsAndSpells;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UserSkill userSkill = null;

    public BasicSkill Skill => userSkill?.skill;

    [SerializeField] private TextMeshProUGUI title = null, keycode = null;

    [SerializeField] private Button btn = null;

    [SerializeField] private Image img = null, coolDownImg = null;

    [SerializeField] private KeyCode quickKey = KeyCode.None;

    [SerializeField] private SkillButtons skillButtons = null;
    private static BasicChar Player => PlayerMain.Player;
    private static BasicChar Target => CombatHandler.Target;
    private bool hovering = false;
    private bool hoverBlockActive = false;
    private float timeStarted;

    private bool _isSkillNotNull;

    // CoolDown

    // Start is called before the first frame update
    private void Start()
    {
        btn.onClick.AddListener(Click);
        keycode.text = quickKey.ToString().Replace("Alpha", string.Empty).Replace(" ", string.Empty);
        if (Skill != null)
        {
            Setup(userSkill);
        }
        else
        {
            title.text = null;
            img.gameObject.SetActive(false);
            coolDownImg.fillAmount = 0;
        }

        _isSkillNotNull = Skill != null;
    }

    private void Click()
    {
        if (_isSkillNotNull)
        {
            if (Skill.HasCoolDown)
            {
                if (!userSkill.Ready) return;

                CombatHandler.PlayerAttack(Skill.Action(Player, Target));
                userSkill.StartCoolDown();
                CoolDownHandler();
                // code to put dim on skill to show it's on cooldown
            }
            else
            {
                CombatHandler.PlayerAttack(Skill.Action(Player, Target));
            }
        }
        else
        {
            skillButtons.ToggleChooseSkill(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(quickKey))
        {
            Click();
        }

        // Delay before starting hoverText
        if (hovering && timeStarted + 0.8f <= Time.unscaledTime && !hoverBlockActive && _isSkillNotNull)
        {
            StartHovering();
        }
    }

    public void Setup(UserSkill addSkill)
    {
        userSkill = addSkill;
        title.text = Skill.Title;
        img.gameObject.SetActive(true);
        img.sprite = Skill.Icon;
        CoolDownHandler();
        _isSkillNotNull = Skill != null;
    }

    public void Clean()
    {
        img.gameObject.SetActive(false);
        userSkill = null;
        title.text = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        StopHoverText();
        skillButtons.ToggleChooseSkill(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isSkillNotNull) return;

        hovering = true;
        timeStarted = Time.unscaledTime;
    }

    public void OnPointerExit(PointerEventData eventData) => StopHoverText();

    private void StartHovering()
    {
        string toSend = Skill.HoverDesc(Player);
        if (Skill.HasCoolDown)
        {
            toSend += $"\nCooldown: {Skill.CoolDown} turns";
            if (!userSkill.Ready)
            {
                toSend += $"\n{userSkill.TurnsLeft} turns left";
            }
        }

        SkillButtonsHoverText.HoverText(toSend);
        hoverBlockActive = true;
    }

    private void StopHoverText()
    {
        hovering = false;
        hoverBlockActive = false;
        SkillButtonsHoverText.StopHovering();
    }

    public void CoolDownHandler() => coolDownImg.fillAmount = Skill.HasCoolDown ? userSkill.CoolDownPercent : 0;
}