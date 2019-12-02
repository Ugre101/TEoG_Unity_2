using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UserSkill userSkill;
    public BasicSkill Skill => userSkill?.skill;
    public TextMeshProUGUI title, keycode;
    public CombatMain combatButtons;
    public Button btn;
    public Image img;
    public KeyCode quickKey;
    public SkillButtons skillButtons;
    public Image coolDownImg;
    private PlayerMain Player => combatButtons.player;
    private BasicChar Target => combatButtons.Target;
    private bool hovering;
    private bool hoverBlockActive = false;
    private float timeStarted;
    // CoolDown

    // Start is called before the first frame update
    private void Start()
    {
        btn.onClick.AddListener(Click);
        if (Skill != null)
        {
            Setup();
        }
        else
        {
            title.text = null;
            img.gameObject.SetActive(false);
            coolDownImg.fillAmount = 0;
        }
    }

    private void Click()
    {
        if (Skill != null)
        {
            if (Skill.HasCoolDown)
            {
                if (userSkill.Ready)
                {
                    combatButtons.PlayerAttack(Skill.Action(Player, Target));
                    userSkill.StartCoolDown();
                    CoolDownHandler();
                    // code to put dim on skill to show it's on cooldown
                }
            }
            else
            {
                combatButtons.PlayerAttack(Skill.Action(Player, Target));
            }
        }
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
                if (!hoverBlockActive && Skill != null)
                {
                    string toSend = $"{Skill.Title}\n{Skill.Type}\nAvg dmg: {Skill.AvgValue}";
                    if (Skill.HasCoolDown)
                    {
                        toSend += $"\nCooldown: {Skill.CoolDown} turns";
                        if (!userSkill.Ready)
                        {
                            toSend += $"\n{userSkill.TurnsLeft} turns left";
                        }
                    }
                    skillButtons.EnableHoverText(toSend);
                    hoverBlockActive = true;
                }
            }
        }
    }

    public void Setup()
    {
        title.text = Skill.Title;
        img.gameObject.SetActive(true);
        img.sprite = Skill.Icon;
        CoolDownHandler();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            StopHoverText();
            skillButtons.ToogleChooseSkill(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Skill != null)
        {
            hovering = true;
            timeStarted = Time.unscaledTime;
        }
    }

    public void OnPointerExit(PointerEventData eventData) => StopHoverText();

    private void StopHoverText()
    {
        hovering = false;
        hoverBlockActive = false;
        skillButtons.DisableHoverText();
    }

    public void CoolDownHandler()
    {
        coolDownImg.fillAmount = Skill.HasCoolDown ? userSkill.CoolDownPercent : 0;
    }
}