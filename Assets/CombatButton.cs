using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatButton : MonoBehaviour
{
    public BasicSkill skill;
    public TextMeshProUGUI text;
    private CombatButtons combatButtons;
    private Button btn;
    private playerMain player => combatButtons.player;
    private BasicChar target => combatButtons.CurrentEnemy;


    // Start is called before the first frame update
    private void Start()
    {
        combatButtons = GetComponentInParent<CombatButtons>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Click);
        text.text = skill.Title;
    }

    private void Click()
    {
        combatButtons.AddToCombatLog(skill.Action(player, target));
    }
}