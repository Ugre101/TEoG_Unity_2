using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NpcInfo : MonoBehaviour
{
    [SerializeField] private Image icon = null;
    [SerializeField] private TextMeshProUGUI infoText = null;

    public void Setup(Npc npc)
    {
        string info = $"{npc.Identity.FirstName}\n{npc.Race(true)}\n{npc.GetGender(true)}";
        infoText.text = info;
    }
}