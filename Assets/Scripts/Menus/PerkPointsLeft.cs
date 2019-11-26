using TMPro;
using UnityEngine;

public class PerkPointsLeft : MonoBehaviour
{
    public playerMain player;
    public TextMeshProUGUI textUGUI;
    private int lastLeft;

    // Start is called before the first frame update
    private void Start()
    {
        if (textUGUI == null)
        {
            if (GetComponent<TextMeshProUGUI>() != null)
            {
                textUGUI = GetComponent<TextMeshProUGUI>();
            }
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMain>();
        }
        lastLeft = player.ExpSystem.PerkPoints;
        textUGUI.text = $"Perkpoints: {lastLeft}";
    }

    // Update is called once per frame
    private void Update()
    {
        if (lastLeft != player.ExpSystem.PerkPoints)
        {
            lastLeft = player.ExpSystem.PerkPoints;
            textUGUI.text = $"Perkpoints: {lastLeft}";
        }
    }
}