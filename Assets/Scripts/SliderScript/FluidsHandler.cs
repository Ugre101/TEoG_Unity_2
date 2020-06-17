using UnityEngine;

public class FluidsHandler : MonoBehaviour
{
    [SerializeField] private PlayerHolder player = null;

    [SerializeField] private GameObject cBar = null, mBar = null;

    private void OnEnable()
    {
        cBar.SetActive(player.BasicChar.SexualOrgans.HaveBalls());
        float milk = player.BasicChar.SexualOrgans.HaveBoobs() ? player.BasicChar.SexualOrgans.Boobs.List.FluidCurrentTotal() : 0;
        mBar.SetActive(player.BasicChar.SexualOrgans.Boobs.Lactating || milk > 0);
    }
}