using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSkillMain : MonoBehaviour
{
    public playerMain player;
    public GameObject container;
    public List<BasicSkill> knowSkills;
    public GameObject prefab, chooseNone;
    public CombatButton combatButton;
    public GameObject hoverBlock;
    public TextMeshProUGUI hoverText;
    public SkillButtons skillButtons;

    [Header("Sorting buttons")]
    public Button physical;

    public Button magical;
    public Button seduction;

    private void Start()
    {
        physical.onClick.AddListener(() => SpawnTypeofSkills(SkillType.Physical));
        magical.onClick.AddListener(() => SpawnTypeofSkills(SkillType.Magical));
        seduction.onClick.AddListener(() => SpawnTypeofSkills(SkillType.Seduction));
    }

    private void OnEnable()
    {
        // Clean container
        transform.KillChildren(container.transform);
        GameObject none = Instantiate(chooseNone, container.transform);
        Button noneBtn = none.GetComponent<Button>();
        noneBtn.onClick.AddListener(() => { CleanSlot(); skillButtons.ToogleButtons(); });
        // Add all skills
        foreach (UserSkill skill in player.Skills)
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
        // Clean container
        transform.KillChildren(container.transform);
        if (knowSkills.Exists(s => s.Type == skillType))
        {
            foreach (UserSkill skill in player.Skills.FindAll(s => s.skill.Type == skillType))
            {
                GameObject option = Instantiate(prefab, container.transform);
                ChooseSkill choose = option.GetComponent<ChooseSkill>();
                choose.Setup(skill, combatButton, hoverBlock, hoverText);
            }
        }
    }

    private void CleanSlot()
    {
        combatButton.img.gameObject.SetActive(false);
        combatButton.userSkill = null;
    }
}