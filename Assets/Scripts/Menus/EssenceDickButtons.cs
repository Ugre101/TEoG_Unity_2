using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceDickButtons : MonoBehaviour
{
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private PlayerMain player = null;
    private Essence Masc => player.Essence.Masc;
    private int lastAmount;
    private TextMeshProUGUI AddText;

    private void OnEnable() => UpdateButtons();

    // Update is called once per frame
    private void Update()
    {
        if (lastAmount != player.SexualOrgans.Dicks.Count)
        {
            UpdateButtons();
        }
    }

    private void UpdateButtons()
    {
        transform.KillChildren();

        GameObject AddDick = Instantiate(prefab, this.transform);
        Button AddBtn = AddDick.GetComponent<Button>();
        AddBtn.onClick.AddListener(AddFunc);
        AddText = AddDick.GetComponentInChildren<TextMeshProUGUI>();
        AddText.text = $"Add dick: {player.SexualOrgans.Dicks.Cost()}";
        foreach (Dick d in player.SexualOrgans.Dicks)
        {
            GameObject pre = Instantiate(prefab, this.transform);
            Button btn = pre.GetComponent<Button>();
            TextMeshProUGUI t = pre.GetComponentInChildren<TextMeshProUGUI>();
            t.text = $"{Settings.MorInch(d.Size)} {d.Cost}Masc";
            btn.onClick.AddListener(() => GrowDick(d, t));
        }
        lastAmount = player.SexualOrgans.Dicks.Count;
    }

    private void GrowDick(Dick d, TextMeshProUGUI t)
    {
        if (Masc.Amount >= d.Cost)
        {
            d.Grow();
            t.text = $"{Settings.MorInch(d.Size)} {d.Cost}Masc";
        }
    }

    private void AddFunc()
    {
        if (Masc.Amount > player.SexualOrgans.Dicks.Cost())
        {
            Masc.Lose(player.SexualOrgans.Dicks.Cost());
            player.SexualOrgans.Dicks.AddDick();
            AddText.text = $"Add dick: {player.SexualOrgans.Dicks.Cost()}";
        }
    }
}