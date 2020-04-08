using UnityEngine;

public class FluidsHandler : MonoBehaviour
{
    [SerializeField] private PlayerMain player = null;

    [SerializeField] private GameObject cBar = null, mBar = null;

    private void OnEnable()
    {
        cBar.SetActive(player.SexualOrgans.HaveBalls());
        float milk = player.SexualOrgans.HaveBoobs() ? player.SexualOrgans.Boobs.FluidCurrentTotal() : 0;
        mBar.SetActive(player.SexualOrgans.Lactating || milk > 0);
    }
}