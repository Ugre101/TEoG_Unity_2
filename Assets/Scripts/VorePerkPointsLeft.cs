using TMPro;
using UnityEngine;

public class VorePerkPointsLeft : MonoBehaviour
{
    private BasicChar player = null;
    [SerializeField] private TextMeshProUGUI proUGUI = null;
    private int lastPoints;
    private bool voreIsActive, started = false;

    // Start is called before the first frame update
    private void Start()
    {
        proUGUI = proUGUI != null ? proUGUI : GetComponent<TextMeshProUGUI>();
        player = player ?? PlayerMain.Player;
        ShowPoints();
        started = true;
        OnEnable();
    }

    private void OnEnable()
    {
        if (started)
        {
            voreIsActive = player.Vore.Active;
            ShowPoints();
            player.Vore.VoreExp.PerkPointsChange += ShowPoints;
        }
    }

    private void OnDisable() => player.Vore.VoreExp.PerkPointsChange -= ShowPoints;

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