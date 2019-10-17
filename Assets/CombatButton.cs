using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CombatButton : MonoBehaviour
{
    public BasicSkill skill;
    public TextMeshProUGUI text;
    private CombatButtons combatButtons;
    private Button btn;
    private playerMain player => combatButtons.player;
    private BasicChar target => combatButtons.CurrentEnemy;
    // Start is called before the first frame update
    void Start()
    {
        combatButtons = GetComponentInParent<CombatButtons>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Click);
        text.text = skill.Title;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void Click()
    {
        combatButtons.AddToCombatLog(skill.Action(player, target));
    }
}
