using TMPro;
using UnityEngine;

public class CombatStatus : MonoBehaviour
{
    public BasicChar whom;
    public TextMeshProUGUI title;
    public HealthSlider healthSlider;
    public WillSlider willSlider;

    public void Setup(BasicChar who)
    {
        whom = who;
        title.text = who.firstName;
        healthSlider.basicChar = who;
        willSlider.basicChar = who;
    }
}