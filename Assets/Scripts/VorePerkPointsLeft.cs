using TMPro;
using UnityEngine;

public class VorePerkPointsLeft : MonoBehaviour
{
    [SerializeField] private PlayerMain player = null;
    [SerializeField] private TextMeshProUGUI proUGUI = null;
    private int lastPoints;
    private bool voreIsActive;

    // Start is called before the first frame update
    private void Start()
    {
        proUGUI = proUGUI != null ? proUGUI : GetComponent<TextMeshProUGUI>();
        ShowPoints();
    }

    private void OnEnable()
    {
        player = player != null ? player : PlayerHolder.Player;
        voreIsActive = player.Vore.Active;
    }

    // Update is called once per frame
    private void Update() => ShowPoints();

    private void ShowPoints()
    {
        if (voreIsActive)
        {
            int perkPoints = player.Vore.VoreExp.PerkPoints;
            if (lastPoints != perkPoints || perkPoints == 0)
            {
                lastPoints = perkPoints;
                proUGUI.text = $"Vore perkpoints: {perkPoints}";
            }
        }
        else
        {
            proUGUI.text = string.Empty;
        }
    }
}