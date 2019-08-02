using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CombatStatus : MonoBehaviour
{
    public BasicChar whom;
    public TextMeshProUGUI name;
    public HealthSlider healthSlider;
    public WillSlider willSlider;
    public void Setup(BasicChar who)
    {
        whom = who;
        name.text = whom.firstName;
        healthSlider.basicChar = whom;
        willSlider.basicChar = whom;
    }
}
