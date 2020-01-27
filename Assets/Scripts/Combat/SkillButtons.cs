using TMPro;
using UnityEngine;

public class SkillButtons : MonoBehaviour
{
    [SerializeField] private GameObject hoverBlock, buttons;
    [SerializeField] private TextMeshProUGUI hoverText;
    [SerializeField] private ChooseSkillMain ChooseSkillMain;

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