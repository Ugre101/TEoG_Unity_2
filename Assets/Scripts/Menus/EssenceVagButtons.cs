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
        if (lastAmount != player.Vaginas.Count)
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
        AddText.text = $"Add vagina: {player.Vaginas.Cost()}Femi";
        foreach (Vagina b in player.Vaginas)
        {
            GameObject pre = Instantiate(prefab, this.transform);
            Button btn = pre.GetComponent<Button>();
            TextMeshProUGUI t = pre.GetComponentInChildren<TextMeshProUGUI>();
            t.text = $"{settings.MorInch(b.Size)} {b.Cost}Femi";
            btn.onClick.AddListener(() => GrowVag(b, t));
        }
        lastAmount = player.Vaginas.Count;
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
        if (player.Femi.Amount > player.Vaginas.Cost())
        {
            player.Femi.Lose(player.Vaginas.Cost());
            player.Vaginas.AddVag();
            AddText.text = $"Add vagina: {player.Vaginas.Cost()}Femi";
        }
    }
}
