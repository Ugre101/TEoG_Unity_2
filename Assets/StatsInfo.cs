using TMPro;
using UnityEngine;

public class StatsInfo : MonoBehaviour
{
    [SerializeField] private BasicChar basicChar = null;
    [SerializeField] private TextMeshProUGUI str = null, charm = null, end = null, will = null, dex = null, intel = null;

    // Start is called before the first frame update
    private void Start()
    {
        basicChar = basicChar != null ? basicChar : PlayerMain.GetPlayer;
        basicChar.Stats.GetAll.ForEach(s => s.ValueChanged += DisplayStats);
    }

    private void DisplayStats()
    {
        StatsContainer stats = basicChar.Stats;
        str.text = $"Strength: {stats.Str}";
        charm.text = $"Charm: {stats.Cha}";
        end.text = $"Endurance: {stats.End}";
        will.text = $"Willpower: {stats.Will}";
        dex.text = $"Dexterity: {stats.Dex}";
        intel.text = $"Intelligence: {stats.Int}";
    }

    // Update is called once per frame
    private void Update()
    {
    }
}