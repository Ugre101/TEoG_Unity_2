using TMPro;
using UnityEngine;

public class CombatStatus : MonoBehaviour
{
    public BasicChar whom;
    public TextMeshProUGUI title;
    public HealthSlider healthSlider;
    public WillSlider willSlider;

    public bool Dead { get; private set; } = false;

    private void OnEnable()
    {
        Health.Died += HasDied;
    }

    public void Setup(BasicChar who)
    {
        whom = who;
        title.text = who.firstName;
        healthSlider.basicChar = who;
        willSlider.basicChar = who;
        Dead = false;
    }

    public void HasDied()
    {
        // When a char dies check if it's the one attached to this script.
        if (whom.HP.Current <= 0 || whom.WP.Current <= 0)
        {
            Dead = true;
        }
        // Change img to show that char is dead/incapitaved
    }

    public void OnDisable()
    {
        Health.Died -= HasDied;
    }
}