using TMPro;
using UnityEngine;

public class DisplayDate : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI display;

    private void OnEnable()
    {
        ShowDate();
        DateSystem.NewHourEvent += ShowDate;
    }

    private void OnDisable() => DateSystem.NewHourEvent -= ShowDate;

    private void ShowDate() => display.text = DateSystem.CurrentDate;
}