using TMPro;
using UnityEngine;

public class StartLoadProgress : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progress = null;

    public void SetProgress(float value) => progress.text = $"Loading progess: {value * 100: 0.##}%";
}