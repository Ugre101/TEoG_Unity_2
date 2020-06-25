using UnityEngine;

public class FluidsHandler : MonoBehaviour
{
    private PlayerMain player => PlayerHolder.Player;

    [SerializeField] private GameObject cumBar = null, milkBar = null, scatBar = null;

    private void OnEnable()
    {
        SexualOrgans so = player.SexualOrgans;

        cumBar.SetActive(so.HaveBalls());

        float milk = so.HaveBoobs() ? so.Boobs.List.FluidCurrentTotal() : 0;
        milkBar.SetActive(so.Boobs.Lactating || milk > 0);

        scatBar.SetActive(Settings.Scat);
    }
}