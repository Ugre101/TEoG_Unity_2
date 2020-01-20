using TMPro;
using UnityEngine;

public class EssenceBallsButtons : MonoBehaviour
{
    [SerializeField] private AddBalls addBallsPrefab = null;
    [SerializeField] private GrowBalls growBallsPrefab = null;
    [SerializeField] private PlayerMain player;
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
        transform.KillChildren();
        Instantiate(addBallsPrefab, transform).Setup(player);
        foreach (Balls b in player.SexualOrgans.Balls)
        {
            Instantiate(growBallsPrefab, transform).Setup(player, b);
        }
        lastAmount = player.SexualOrgans.Balls.Count;
    }
}