using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatStatus : MonoBehaviour
{
    private BasicChar whom;
    public TextMeshProUGUI title;
    public HealthSlider healthSlider;
    public WillSlider willSlider;
    private CombatTeam team;
    public bool Dead { get; private set; } = false;
    public Button btn;
    public GameObject frame;
    private void Start()
    {
        btn.onClick.AddListener(Select);
    }
    private void OnEnable()
    {
        Health.Died += HasDied;
    }

    public void Setup(BasicChar who, CombatTeam combatTeam)
    {
        whom = who;
        title.text = who.firstName;
        healthSlider.basicChar = who;
        willSlider.basicChar = who;
        Dead = false;
        team = combatTeam;
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
        Health.Died -= HasDied;
    }
    public void Select()
    {
        frame.SetActive(true);
    }
}