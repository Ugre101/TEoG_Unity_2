using SkillsAndSpells;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseSkill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Button btn = null;

    [SerializeField]
    private Image img = null;

    private UserSkill userSkill;
    private BasicSkill Skill => userSkill.skill;
    private CombatButton target;
    private GameObject hoverBlock;
    private TextMeshProUGUI hoverText;

    // Start is called before the first frame update
    private void Start() => btn.onClick.AddListener(Click);

    public void Click() => target.Setup(userSkill);

    public void Setup(UserSkill basicSkill, CombatButton combatButton, GameObject hover, TextMeshProUGUI text, UnityEngine.Events.UnityAction func)
    {
        userSkill = basicSkill;
        target = combatButton;
        hoverBlock = hover;
        hoverText = text;
        img.sprite = Skill.Icon;
        btn.onClick.AddListener(func);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverBlock.SetActive(true);
        hoverText.text = $"{Skill.Title}";
    }

    public void OnPointerExit(PointerEventData eventData) => hoverBlock.SetActive(false);
}