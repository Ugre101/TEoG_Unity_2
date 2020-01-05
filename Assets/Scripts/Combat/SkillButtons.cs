using TMPro;
using UnityEngine;

public class SkillButtons : MonoBehaviour
{
    public GameObject hoverBlock, buttons;
    public TextMeshProUGUI hoverText;
    public ChooseSkillMain ChooseSkillMain;

    public void ToogleChooseSkill(CombatButton target)
    {
        ChooseSkillMain.Toggle(target);
        buttons.SetActive(false);
    }

    public void ToogleButtons()
    {
        ChooseSkillMain.gameObject.SetActive(false);
        buttons.SetActive(true);
    }
}