using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceVagButtons : MonoBehaviour
{
    public GameObject prefab;
    public playerMain player;
    public Settings settings;
    private int lastAmount;
    private TextMeshProUGUI AddText;

    // Start is called before the first frame update
    private void OnEnable()
    {
        UpdateButtons();
    }

    // Update is called once per frame
    private void Update()
    {
        if (lastAmount != player.SexualOrgans.Vaginas.Count)
        {
            UpdateButtons();
        }
    }

    private void UpdateButtons()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject AddVag = Instantiate(prefab, this.transform);
        Button AddBtn = AddVag.GetComponent<Button>();
        AddBtn.onClick.AddListener(AddFunc);
        AddText = AddVag.GetComponentInChildren<TextMeshProUGUI>();
        AddText.text = $"Add vagina: {player.SexualOrgans.Vaginas.Cost()}Femi";
        foreach (Vagina b in player.SexualOrgans.Vaginas)
        {
            GameObject pre = Instantiate(prefab, this.transform);
            Button btn = pre.GetComponent<Button>();
            TextMeshProUGUI t = pre.GetComponentInChildren<TextMeshProUGUI>();
            t.text = $"{settings.MorInch(b.Size)} {b.Cost}Femi";
            btn.onClick.AddListener(() => GrowVag(b, t));
        }
        lastAmount = player.SexualOrgans.Vaginas.Count;
    }

    private void GrowVag(Vagina b, TextMeshProUGUI t)
    {
        if (player.Femi.Amount >= b.Cost)
        {
            b.Grow();
            t.text = $"{settings.MorInch(b.Size)} {b.Cost}Femi";
        }
    }

    private void AddFunc()
    {
        if (player.Femi.Amount > player.SexualOrgans.Vaginas.Cost())
        {
            player.Femi.Lose(player.SexualOrgans.Vaginas.Cost());
            player.SexualOrgans.Vaginas.AddVag();
            AddText.text = $"Add vagina: {player.SexualOrgans.Vaginas.Cost()}Femi";
        }
    }
}
