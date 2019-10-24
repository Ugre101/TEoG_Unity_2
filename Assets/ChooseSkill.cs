using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseSkill : MonoBehaviour
{
    public Button btn;
    private BasicSkill skill;
    private CombatButton target;
    public Image img;
    // Start is called before the first frame update
    private void Start()
    {
        btn.onClick.AddListener(Click);
    }

    public void Click()
    {
        target.skill = skill;
        target.Setup();
    }
    public void Setup(BasicSkill basicSkill,CombatButton combatButton)
    {
        skill = basicSkill;
        target = combatButton;
        img.sprite = skill.Icon;
    }
}
