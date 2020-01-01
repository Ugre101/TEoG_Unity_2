using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSkillMain : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private PlayerMain player = null;

    [SerializeField]
    private GameObject container = null;

    [SerializeField]
    private List<BasicSkill> knowSkills = new List<BasicSkill>();

    [SerializeField]
    private ChooseSkill prefab = null;

    [SerializeField]
    private Button chooseNone = null;

    private CombatButton combatButton = null;

    [SerializeField]
    private GameObject hoverBlock = null;

    [SerializeField]
    private TextMeshProUGUI hoverText = null;

    [SerializeField]
    private SkillButtons skillButtons = null;

    [SerializeField]
    private SkillBook skillBook = null;

    [Header("Sorting buttons")]
    [SerializeField]
    private Button physical, magical, seduction;

    #endregion Properties

    private void Start()
    {
        physical.onClick.AddListener(() => SpawnTypeofSkills(SkillType.Physical));
        magical.onClick.AddListener(() => SpawnTypeofSkills(SkillType.Magical));
        seduction.onClick.AddListener(() => SpawnTypeofSkills(SkillType.Seduction));
    }

    public void Toggle(CombatButton parCombatBtn)
    {
        if (player == null) { player = PlayerMain.GetPlayer; }
        gameObject.SetActive(true);
        combatButton = parCombatBtn;
        // Clean container
        transform.KillChildren(container.transform);
        Button noneBtn = Instantiate(chooseNone, container.transform);
        noneBtn.onClick.AddListener(() => { combatButton.Clean(); skillButtons.ToogleButtons(); });
        // Add all skills
        foreach (Skill skill in player.Skills)
        {
            ChooseSkill choose = Instantiate(prefab, container.transform);
            choose.Setup(skillBook.Dict.Match(skill.Id), combatButton, hoverBlock, hoverText, skillButtons.ToogleButtons);
        }
    }

    private void SpawnTypeofSkills(SkillType skillType)
    {
        // Clean container
        transform.KillChildren(container.transform);
        if (knowSkills.Exists(s => s.Type == skillType))
        {
            List<UserSkill> mySkills = skillBook.Dict.OwnedSkills(player.Skills);
            foreach (UserSkill skill in mySkills.FindAll(s => s.skill.Type == skillType))
            {
                ChooseSkill choose = Instantiate(prefab, container.transform);
                choose.Setup(skill, combatButton, hoverBlock, hoverText, skillButtons.ToogleButtons);
            }
        }
    }
}