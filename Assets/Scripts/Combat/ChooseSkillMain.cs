using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSkillMain : MonoBehaviour
{
    #region Properties

    [SerializeField] private PlayerMain player = null;

    [SerializeField] private GameObject container = null;

    [SerializeField] private List<BasicSkill> knowSkills = new List<BasicSkill>();

    [SerializeField] private ChooseSkill prefab = null;

    [SerializeField] private Button chooseNone = null;

    private CombatButton combatButton = null;

    [SerializeField] private GameObject hoverBlock = null;

    [SerializeField] private TextMeshProUGUI hoverText = null;

    [SerializeField] private SkillButtons skillButtons = null;

    [SerializeField] private SkillBook skillBook = null;

    [Header("Sorting buttons")]
    [SerializeField] private Button physical = null, magical = null, seduction = null;

    #endregion Properties

    private void Start()
    {
        physical.onClick.AddListener(() => SpawnTypeofSkills(SkillType.Physical));
        magical.onClick.AddListener(() => SpawnTypeofSkills(SkillType.Magical));
        seduction.onClick.AddListener(() => SpawnTypeofSkills(SkillType.Seduction));
    }

    public void Toggle(CombatButton parCombatBtn)
    {
        player = player != null ? player : PlayerMain.GetPlayer;
        gameObject.SetActive(true);
        combatButton = parCombatBtn;
        // Clean container
        container.transform.KillChildren();
        Instantiate(chooseNone, container.transform).onClick.AddListener(() => { combatButton.Clean(); skillButtons.ToogleButtons(); });
        // Add all skills
        foreach (Skill skill in player.Skills)
        {
            Instantiate(prefab, container.transform).Setup(skillBook.Dict.Match(skill.Id), combatButton, hoverBlock, hoverText, skillButtons.ToogleButtons);
        }
    }

    private void SpawnTypeofSkills(SkillType skillType)
    {
        // Clean container
        container.transform.KillChildren();
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