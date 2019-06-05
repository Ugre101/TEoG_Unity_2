using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceDickButtons : MonoBehaviour
{
    public GameObject prefab;
    public playerMain player;
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
        if (lastAmount != player.Dicks.Count)
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
        GameObject AddDick = Instantiate(prefab, this.transform);
        Button AddBtn = AddDick.GetComponent<Button>();
        AddBtn.onClick.AddListener(AddFunc);
        AddText = AddDick.GetComponentInChildren<TextMeshProUGUI>();
        AddText.text = $"Add dick: {player.DickCost()}";
        foreach (Dick d in player.Dicks)
        {
            GameObject pre = Instantiate(prefab, this.transform);
            Button btn = pre.GetComponent<Button>();
            TextMeshProUGUI t = pre.GetComponentInChildren<TextMeshProUGUI>();
            t.text = $"Dick {d.Size}";
            btn.onClick.AddListener(() => GrowDick(d, t));
        }
        lastAmount = player.Dicks.Count;
    }

    private void GrowDick(Dick d, TextMeshProUGUI t)
    {
        if (player.Masc.Amount >= d.Cost)
        {
            d.Grow();
            t.text = $"Dick {d.Size}";
        }
    }

    private void AddFunc()
    {
        if (player.Masc.Amount > player.DickCost())
        {
            player.Masc.Lose(player.DickCost());
            player.AddDick();
            AddText.text = $"Add dick: {player.DickCost()}";
        }
    }
}