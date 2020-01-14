using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EssenceBallsButtons : MonoBehaviour
{
    public GameObject prefab;
    public PlayerMain player;
    private Essence Masc => player.Essence.Masc;
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
        if (lastAmount != player.SexualOrgans.Balls.Count)
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
        GameObject AddBalls = Instantiate(prefab, this.transform);
        Button AddBtn = AddBalls.GetComponent<Button>();
        AddBtn.onClick.AddListener(AddFunc);
        AddText = AddBalls.GetComponentInChildren<TextMeshProUGUI>();
        AddText.text = $"Add balls: {player.SexualOrgans.Balls.Cost()}";
        foreach (Balls b in player.SexualOrgans.Balls)
        {
            GameObject pre = Instantiate(prefab, this.transform);
            Button btn = pre.GetComponent<Button>();
            TextMeshProUGUI t = pre.GetComponentInChildren<TextMeshProUGUI>();
            t.text = $"{Settings.MorInch(b.Size)} {b.Cost}Masc";
            btn.onClick.AddListener(() => GrowBalls(b, t));
        }
        lastAmount = player.SexualOrgans.Balls.Count;
    }

    private void GrowBalls(Balls b, TextMeshProUGUI t)
    {
        if (Masc.Amount >= b.Cost)
        {
            b.Grow();
            t.text = $"{Settings.MorInch(b.Size)} {b.Cost}Masc";
        }
    }

    private void AddFunc()
    {
        if (Masc.Amount > player.SexualOrgans.Balls.Cost())
        {
            Masc.Lose(player.SexualOrgans.Balls.Cost());
            player.SexualOrgans.Balls.AddBalls();
            AddText.text = $"Add balls: {player.SexualOrgans.Balls.Cost()}Masc";
        }
    }
}