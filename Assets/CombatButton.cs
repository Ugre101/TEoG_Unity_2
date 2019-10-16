using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CombatButton : MonoBehaviour
{
    public BasicSkill skill;
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
