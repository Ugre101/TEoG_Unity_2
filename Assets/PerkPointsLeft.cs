using UnityEngine;
using TMPro;

public class PerkPointsLeft : MonoBehaviour
{
    public playerMain player;
    private TextMeshProUGUI textUGUI;
    private int lastLeft;
    // Start is called before the first frame update
    void Start()
    {
        textUGUI = GetComponent<TextMeshProUGUI>();
        if (textUGUI == null || player == null)
        {
            this.GetComponent<PerkPointsLeft>().enabled = false;
        }
        lastLeft = player.PerkPoints;
        textUGUI.text = $"Perkpoints: {lastLeft}";
    }

    // Update is called once per frame
    void Update()
    {
        if (lastLeft != player.PerkPoints)
        {
            lastLeft = player.PerkPoints;
            textUGUI.text = $"Perkpoints: {lastLeft}";
        }
    }
}
