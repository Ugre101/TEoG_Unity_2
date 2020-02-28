using UnityEngine;

public class SkillButtons : MonoBehaviour
{
    [SerializeField] private GameObject buttons = null;
    [SerializeField] private ChooseSkillMain ChooseSkillMain = null;

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