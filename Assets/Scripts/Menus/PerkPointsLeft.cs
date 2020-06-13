using TMPro;
using UnityEngine;

public class PerkPointsLeft : MonoBehaviour
{
    [SerializeField] private PlayerMain player = null;
    [SerializeField] private TextMeshProUGUI textUGUI = null;
    private int lastLeft;

    // Start is called before the first frame update
    private void Start()
    {
        textUGUI = textUGUI != null ? textUGUI : GetComponent<TextMeshProUGUI>();
        player = player != null ? player : PlayerHolder.Player;
        ShowPoints();
    }

    // Update is called once per frame
    private void Update() => ShowPoints();

    private void ShowPoints()
    {
        int perkPoints = player.ExpSystem.PerkPoints;
        if (lastLeft != perkPoints)
        {
            lastLeft = perkPoints;
            textUGUI.text = $"Perkpoints: {lastLeft}";
        }
    }
}