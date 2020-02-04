using System.Linq;
using TMPro;
using UnityEngine;

public class DisplayLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText = null;
    [SerializeField] private BasicChar basicChar = null;

    private int Level => (basicChar.Stats.GetAll.Sum(s => s.BaseValue) - 22) / 4;

    // Start is called before the first frame update
    private void Start()
    {
        levelText = levelText != null ? levelText : GetComponentInChildren<TextMeshProUGUI>();
        basicChar = basicChar != null ? basicChar : GetComponentInParent<BasicChar>();
        basicChar.Stats.GetAll.ForEach(s => s.ValueChanged += StatChange);
        StatChange();
    }

    private void OnDestroy() => basicChar.Stats.GetAll.ForEach(s => s.ValueChanged -= StatChange);

    private void StatChange() => levelText.text = $"Level: {Level}";
}