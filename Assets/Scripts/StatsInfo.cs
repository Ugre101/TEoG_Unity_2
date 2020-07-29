using TMPro;
using UnityEngine;

public class StatsInfo : MonoBehaviour
{
    [SerializeField] private BasicChar basicChar = null;
    [SerializeField] private TextMeshProUGUI str = null, charm = null, end = null, will = null, dex = null, intel = null, hp = null, wp = null;

    private void OnEnable()
    {
        basicChar = basicChar ?? PlayerHolder.Player;
        basicChar.Stats.GetAll.ForEach(s => s.ValueChanged += DisplayStats);
        DisplayStats();
    }

    private void OnDisable() => basicChar.Stats.GetAll.ForEach(s => s.ValueChanged -= DisplayStats);

    private void DisplayStats()
    {
        StatsContainer stats = basicChar.Stats;
        str.text = $"Strength: {stats.Str}";
        charm.text = $"Charm: {stats.Cha}";
        end.text = $"Endurance: {stats.End}";
        will.text = $"Willpower: {stats.Will}";
        dex.text = $"Dexterity: {stats.Dex}";
        intel.text = $"Intelligence: {stats.Int}";
        hp.text = $"Health: {basicChar.HP.Value}";
        wp.text = $"Will health: {basicChar.WP.Value}";
    }
}