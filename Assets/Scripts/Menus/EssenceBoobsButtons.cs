using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceBoobsButtons : MonoBehaviour
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
        if (lastAmount != player.Boobs.Count)
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
        GameObject AddBoobs = Instantiate(prefab, this.transform);
        Button AddBtn = AddBoobs.GetComponent<Button>();
        AddBtn.onClick.AddListener(AddFunc);
        AddText = AddBoobs.GetComponentInChildren<TextMeshProUGUI>();
        AddText.text = $"Add boobs: {player.Boobs.Cost()}Femi";
        foreach (Boobs b in player.Boobs)
        {
            GameObject pre = Instantiate(prefab, this.transform);
            Button btn = pre.GetComponent<Button>();
            TextMeshProUGUI t = pre.GetComponentInChildren<TextMeshProUGUI>();
            t.text = $"{settings.MorInch(b.Size)} {b.Cost}Femi";
            btn.onClick.AddListener(() => GrowBoobs(b, t));
        }
        lastAmount = player.Boobs.Count;
    }

    private void GrowBoobs(Boobs b, TextMeshProUGUI t)
    {
        if (player.Femi.Amount >= b.Cost)
        {
            b.Grow();
            t.text = $"{settings.MorInch(b.Size)} {b.Cost}Femi";
        }
    }

    private void AddFunc()
    {
        if (player.Femi.Amount > player.Boobs.Cost())
        {
            player.Femi.Lose(player.Boobs.Cost());
            player.Boobs.AddBoobs();
            AddText.text = $"Add boobs: {player.Boobs.Cost()}Femi";
        }
    }
}
