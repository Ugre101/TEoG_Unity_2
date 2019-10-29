using TMPro;
using UnityEngine;

public class SkillButtons : MonoBehaviour
{
    public GameObject hoverBlock, buttons;
    public TextMeshProUGUI hoverText;
    public ChooseSkillMain ChooseSkillMain;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void EnableHoverText(string text)
    {
        hoverBlock.SetActive(true);
        hoverText.text = text;
    }

    public void DisableHoverText()
    {
        hoverText.text = string.Empty;
        hoverBlock.SetActive(false);
    }

    public void ToogleChooseSkill(CombatButton target)
    {
        ChooseSkillMain.combatButton = target;
        buttons.SetActive(false);
        ChooseSkillMain.gameObject.SetActive(true);
    }

    public void ToogleButtons()
    {
        ChooseSkillMain.gameObject.SetActive(false);
        buttons.SetActive(true);
    }
}
