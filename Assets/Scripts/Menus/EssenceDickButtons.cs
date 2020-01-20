using UnityEngine;

public class EssenceDickButtons : MonoBehaviour
{
    [SerializeField] private AddDick addDickPrefab = null;
    [SerializeField] private GrowDick growDickPrefab = null;
    [SerializeField] private PlayerMain player = null;
    private int lastAmount;

    private void OnEnable() => UpdateButtons();

    // Update is called once per frame
    private void Update()
    {
        if (lastAmount != player.SexualOrgans.Dicks.Count)
        {
            UpdateButtons();
        }
    }

    private void UpdateButtons()
    {
        transform.KillChildren();
        Instantiate(addDickPrefab, transform).Setup(player);
        foreach (Dick d in player.SexualOrgans.Dicks)
        {
            Instantiate(growDickPrefab, transform).Setup(player, d);
        }
        lastAmount = player.SexualOrgans.Dicks.Count;
    }
}