using UnityEngine;
using UnityEngine.Serialization;

public class SkillButtons : MonoBehaviour
{
    [SerializeField] private GameObject buttons = null;
    [FormerlySerializedAs("ChooseSkillMain")] [SerializeField] private ChooseSkillMain chooseSkillMain = null;

    public void ToggleChooseSkill(CombatButton target)
    {
        chooseSkillMain.Toggle(target);
        buttons.SetActive(false);
    }

    public void ToggleButtons()
    {
        chooseSkillMain.gameObject.SetActive(false);
        buttons.SetActive(true);
    }
}