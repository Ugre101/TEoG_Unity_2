using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatStatus : MonoBehaviour
{
    private BasicChar whom;
    public bool Dead { get; private set; } = false;

    [SerializeField] private TextMeshProUGUI title = null;

    [SerializeField] private HealthSlider healthSlider = null;

    [SerializeField] private WillSlider willSlider = null;

    [SerializeField] private Button btn = null;

    [SerializeField] private GameObject frame = null;

    private CombatTeam team = null;

    private CombatMain combatMain = null;

    public void Setup(BasicChar who, CombatTeam combatTeam, CombatMain main)
    {
        whom = who;
        title.text = whom.Identity.FirstName;
        healthSlider.Setup(whom);
        willSlider.Setup(whom);
        Dead = false;
        team = combatTeam;
        combatMain = main;
        btn.onClick.AddListener(Select);
        whom.Hp.DeadEvent += HasDied;
        whom.Wp.DeadEvent += HasDied;
    }

    private void HasDied()
    {
        Dead = true;
        team.WeLost();
    }

    public void OnDisable()
    {
        whom.Hp.DeadEvent -= HasDied;
        whom.Wp.DeadEvent -= HasDied;
    }

    private void OnDestroy()
    {
        whom.Hp.DeadEvent -= HasDied;
        whom.Wp.DeadEvent -= HasDied;
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