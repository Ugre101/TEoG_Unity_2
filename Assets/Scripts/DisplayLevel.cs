using TMPro;
using UnityEngine;

public class DisplayLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText = null;
    [SerializeField] private CharHolder charHolder = null;
    private BasicChar BasicChar => charHolder.BasicChar;

    private int Level => BasicChar.Stats.CalcLevelByStatTotal();

    // Start is called before the first frame update
    private void Start()
    {
        levelText = levelText != null ? levelText : GetComponentInChildren<TextMeshProUGUI>();
        charHolder = charHolder != null ? charHolder : GetComponentInParent<CharHolder>();
        BasicChar.Stats.GetAll.ForEach(s => s.ValueChanged += StatChange);
        BasicChar.DestroyHolderEvent += OnDestroy;
        StatChange();
    }

    private void OnDestroy() => BasicChar.Stats.GetAll.ForEach(s => s.ValueChanged -= StatChange);

    private void StatChange() => levelText.text = $"Level: {Level}";
}