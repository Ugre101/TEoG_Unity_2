using System.Collections.Generic;
using UnityEngine;

public class ChooseSkillMain : MonoBehaviour
{
    public GameObject container;
    public List<BasicSkill> knowSkills;
    public GameObject prefab;
    public CombatButton combatButton;

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
            choose.Setup(skill, combatButton);
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
                choose.Setup(skill, combatButton);
            }
        }
    }
}