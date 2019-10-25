using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ChooseSkillMain : MonoBehaviour
{
    public GameObject container;
    public List<BasicSkill> knowSkills;
    public GameObject prefab;
    public CombatButton combatButton;
    public GameObject hoverBlock;
    public TextMeshProUGUI hoverText;
    public SkillButtons skillButtons;
    private void OnEnable()
    {
        // Clean container
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
        // Add all skills
        foreach (BasicSkill skill in knowSkills)
        {
            GameObject option = Instantiate(prefab, container.transform);
            ChooseSkill choose = option.GetComponent<ChooseSkill>();
            choose.Setup(skill, combatButton, hoverBlock, hoverText);
            Button btn = option.GetComponent<Button>();
            btn.onClick.AddListener(skillButtons.ToogleButtons);
        }
    }

    public void SpawnTypeofSkills(SkillType skillType)
    {
        if (knowSkills.Exists(s => s.Type == skillType))
        {
            foreach (BasicSkill skill in knowSkills.FindAll(s => s.Type == skillType))
            {
                GameObject option = Instantiate(prefab, container.transform);
                ChooseSkill choose = option.GetComponent<ChooseSkill>();
                choose.Setup(skill, combatButton, hoverBlock, hoverText);
            }
        }
    }
}