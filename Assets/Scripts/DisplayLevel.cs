using TMPro;
using UnityEngine;

public class DisplayLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText = null;
    [SerializeField] private CharHolder charHolder = null;
    private BasicChar BasicChar => charHolder.BasicChar;

    private int Level => BasicChar.Stats.CalcLevelByStatTotal();

    private bool firstStart = true;

    // Start is called before the first frame update
    private void Start()
    {
        if (firstStart)
        {
            levelText = levelText != null ? levelText : GetComponentInChildren<TextMeshProUGUI>();
            charHolder = charHolder != null ? charHolder : GetComponentInParent<CharHolder>();
            BasicChar.Stats.GetAll.ForEach(s => s.ValueChanged += StatChange);
            BasicChar.DestroyHolderEvent += BeforeDestroy;
            StatChange();
            firstStart = false;
        }
    }

    private void OnEnable()
    {
        if (firstStart)
            Start();
        BasicChar.Stats.GetAll.ForEach(s => s.ValueChanged += StatChange);
        StatChange();
    }

    private void OnDisable() => BasicChar.Stats.GetAll.ForEach(s => s.ValueChanged -= StatChange);

    private void BeforeDestroy() => BasicChar.Stats.GetAll.ForEach(s => s.ValueChanged -= StatChange);

    private void StatChange() => levelText.text = $"Level: {Level}";
}