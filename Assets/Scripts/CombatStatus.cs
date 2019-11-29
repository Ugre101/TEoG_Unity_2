using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatStatus : MonoBehaviour
{
    public BasicChar whom;
    public bool Dead { get; private set; } = false;

    [SerializeField]
    private TextMeshProUGUI title = null;

    [SerializeField]
    private HealthSlider healthSlider = null;

    [SerializeField]
    private WillSlider willSlider = null;

    [SerializeField]
    private Button btn = null;

    [SerializeField]
    private GameObject frame = null;

    private CombatTeam team = null;

    [SerializeField]
    private CombatMain combatMain = null;

    public void Setup(BasicChar who, CombatTeam combatTeam, CombatMain main)
    {
        whom = who;
        title.text = whom.firstName;
        healthSlider.Setup(whom);
        willSlider.Setup(whom);
        Dead = false;
        team = combatTeam;
        combatMain = main;
        btn.onClick.AddListener(Select);
        Health.DeadEvent += HasDied;
    }

    public void HasDied()
    {
        // When a char dies check if it's the one attached to this script.
        if (whom.HP.Current <= 0 || whom.WP.Current <= 0)
        {
            Dead = true;
            team.WeLost();
        }
        // Change img to show that char is dead/incapitaved
    }

    public void OnDisable()
    {
        Health.DeadEvent -= HasDied;
    }
    private void OnDestroy()
    {
        Health.DeadEvent -= HasDied;
    }
    /// <summary>
    /// Click once to select and twice to deselect
    /// </summary>
    public void Select()
    {
        bool active = frame.activeSelf;
        combatMain.SelectNewTarget(active ? null : whom);
        frame.SetActive(!active);
    }

    public void DeSelect()
    {
        frame.SetActive(false);
    }
}